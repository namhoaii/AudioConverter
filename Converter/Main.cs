using System.Diagnostics;
using Converter.Utils;

namespace Converter
{
    public partial class Main : Form
    {
        List<string> items = new List<string>();
        private CancellationTokenSource _cts;


        public Main()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (items.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn thư mục chứa các file cần chuyển đổi.");
                return;
            }
            if (txtPathSave.Text == "")
            {
                MessageBox.Show("Vui lòng chọn thư mục lưu file đã chuyển đổi.");
                return;
            }

            pnlControl.Enabled = false;
            pnlProgress.Visible = true;

            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            btnStart.Enabled = false;
            btnStart.Loading = true;

            string inputFolder = txtPath.Text;
            string outputFolder = txtPathSave.Text;
            string ffmpegPath = "ffmpeg.exe";
            //string ffmpegPath = Path.Combine(Application.StartupPath, "ffmpeg.exe");

            int steps = items.Count;
            int stepGap = 2;
            int progressWidth = progressing.Width;

            int totalGap = (steps - 1) * stepGap;
            int stepSize = (progressWidth - totalGap) / steps;

            progressing.Steps = steps;
            progressing.StepGap = stepGap;
            progressing.StepSize = stepSize;
            progressing.Value = 0;
            progressing.Loading = true;

            int i = 0;
            int maxParallelTasks = int.TryParse(sltTask.SelectedValue?.ToString(), out var result) ? result : 1;

            try
            {
                using (SemaphoreSlim semaphore = new SemaphoreSlim(maxParallelTasks))
                {
                    List<Task> tasks = new List<Task>();

                    foreach (string inputFile in items)
                    {
                        await semaphore.WaitAsync(token);

                        tasks.Add(Task.Run(async () =>
                        {
                            try
                            {
                                string fileName = Path.GetFileNameWithoutExtension(inputFile);
                                if (chkNoVie.Checked)
                                {
                                    fileName = StringFormater.ConvertToSlug(fileName);
                                }
                                string outputPath = Path.Combine(outputFolder, fileName + ".mp3");

                                await ConvertMp4aToMp3Async(ffmpegPath, inputFile, outputPath, token);

                                int current = Interlocked.Increment(ref i);

                                Invoke(() =>
                                {
                                    progressing.Text = $"{current}/{items.Count}";
                                    progressing.Value = (float)current / items.Count;
                                });
                            }
                            catch (Exception ex)
                            {
                                if (ex is OperationCanceledException)
                                    return; // Bỏ qua, không thông báo

                                Invoke(() =>
                                {
                                    MessageBox.Show($"❌ Lỗi với file {inputFile}\n{ex.Message}");
                                });
                            }

                            finally
                            {
                                semaphore.Release();
                            }
                        }));
                    }

                    await Task.WhenAll(tasks);
                }

                MessageBox.Show("Hoàn tất chuyển đổi!");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Quá trình đã bị dừng bởi người dùng.");
            }
            finally
            {
                btnStart.Enabled = true;
                btnStart.Loading = false;
                pnlControl.Enabled = true;
                pnlProgress.Visible = false;
            }

           
        }


        public static async Task ConvertMp4aToMp3Async(string ffmpegPath, string inputPath, string outputPath, CancellationToken token)
        {
            var args = $"-y -i \"{inputPath}\" -codec:a libmp3lame -qscale:a 2 \"{outputPath}\"";

            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                process.EnableRaisingEvents = true;

                var tcs = new TaskCompletionSource<bool>();

                process.Exited += (sender, e) => tcs.TrySetResult(true);

                process.Start();

                // Khi người dùng cancel, tiến hành kill ffmpeg
                using (token.Register(() =>
                {
                    try
                    {
                        if (!process.HasExited)
                            process.Kill();
                    }
                    catch { /* ignore nếu đã thoát */ }
                }))
                {
                    string errorOutput = await process.StandardError.ReadToEndAsync();

                    await tcs.Task; // chờ ffmpeg kết thúc

                    if (process.ExitCode != 0 && !token.IsCancellationRequested)
                    {
                        throw new Exception($"❌ Lỗi khi chuyển file: {Path.GetFileName(inputPath)}\n\n{errorOutput}");
                    }

                    token.ThrowIfCancellationRequested(); // báo lỗi nếu đã bị hủy
                }
            }
        }

        private void btnGetPath_Click(object sender, EventArgs e)
        {
            items.Clear();
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Chọn thư mục";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = folderDialog.SelectedPath;
                    string[] allFiles = Directory.GetFiles(folderDialog.SelectedPath, "*.*", SearchOption.TopDirectoryOnly);

                    // Hiển thị từng file
                    foreach (string filePath in allFiles)
                    {
                        items.Add(filePath);
                    }
                }
            }
            MessageBox.Show($"Đã lấy {items.Count} file từ thư mục: {txtPath.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPathSave_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Chọn thư mục";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathSave.Text = folderDialog.SelectedPath;

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int maxParallelTasks = Math.Clamp(Environment.ProcessorCount / 2, 2, 6);
            for (int i = 1; i <= maxParallelTasks; i++)
            {
                sltTask.Items.Add(i);
            }
            sltTask.SelectedIndex = 0;

        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            var isClose = MessageBox.Show("Bạn có chắc chắn muốn dừng quá trình chuyển đổi không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (isClose != DialogResult.Yes)
                return;

            _cts?.Cancel();
            pnlControl.Enabled = true;
            pnlProgress.Visible = false;
        }
    }
}
