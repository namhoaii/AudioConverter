namespace Converter
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            progressing = new AntdUI.Progress();
            openFileDialog1 = new OpenFileDialog();
            btnEnd = new AntdUI.Button();
            pnlProgress = new AntdUI.Panel();
            btnGetPath = new AntdUI.Button();
            txtPath = new AntdUI.Input();
            txtPathSave = new AntdUI.Input();
            btnStart = new AntdUI.Button();
            btnPathSave = new AntdUI.Button();
            chkNoVie = new AntdUI.Checkbox();
            sltTask = new AntdUI.Select();
            label1 = new AntdUI.Label();
            pnlControl = new AntdUI.Panel();
            pnlProgress.SuspendLayout();
            pnlControl.SuspendLayout();
            SuspendLayout();
            // 
            // progressing
            // 
            progressing.Location = new Point(23, 19);
            progressing.Name = "progressing";
            progressing.Size = new Size(429, 40);
            progressing.TabIndex = 4;
            progressing.Text = "progress1";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnEnd
            // 
            btnEnd.Location = new Point(458, 19);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(93, 40);
            btnEnd.TabIndex = 3;
            btnEnd.Text = "Dừng";
            btnEnd.Click += btnEnd_Click;
            // 
            // pnlProgress
            // 
            pnlProgress.Back = SystemColors.Control;
            pnlProgress.Controls.Add(progressing);
            pnlProgress.Controls.Add(btnEnd);
            pnlProgress.Dock = DockStyle.Fill;
            pnlProgress.Location = new Point(0, 167);
            pnlProgress.Name = "pnlProgress";
            pnlProgress.Size = new Size(565, 84);
            pnlProgress.TabIndex = 9;
            pnlProgress.Text = "panel1";
            pnlProgress.Visible = false;
            // 
            // btnGetPath
            // 
            btnGetPath.Location = new Point(23, 12);
            btnGetPath.Name = "btnGetPath";
            btnGetPath.Size = new Size(114, 40);
            btnGetPath.TabIndex = 1;
            btnGetPath.Text = "Chọn thư mục";
            btnGetPath.Click += btnGetPath_Click;
            // 
            // txtPath
            // 
            txtPath.Location = new Point(143, 12);
            txtPath.Name = "txtPath";
            txtPath.ReadOnly = true;
            txtPath.Size = new Size(192, 40);
            txtPath.TabIndex = 2;
            // 
            // txtPathSave
            // 
            txtPathSave.Location = new Point(143, 58);
            txtPathSave.Name = "txtPathSave";
            txtPathSave.ReadOnly = true;
            txtPathSave.Size = new Size(192, 40);
            txtPathSave.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(242, 114);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(93, 40);
            btnStart.TabIndex = 3;
            btnStart.Text = "Bắt đầu";
            btnStart.Click += btnStart_Click;
            // 
            // btnPathSave
            // 
            btnPathSave.Location = new Point(23, 58);
            btnPathSave.Name = "btnPathSave";
            btnPathSave.Size = new Size(114, 40);
            btnPathSave.TabIndex = 5;
            btnPathSave.Text = "Nơi lưu trữ";
            btnPathSave.Click += btnPathSave_Click;
            // 
            // chkNoVie
            // 
            chkNoVie.BackColor = Color.Transparent;
            chkNoVie.Location = new Point(356, 58);
            chkNoVie.Name = "chkNoVie";
            chkNoVie.Size = new Size(195, 40);
            chkNoVie.TabIndex = 6;
            chkNoVie.Text = "Bỏ dấu tiếng việt";
            // 
            // sltTask
            // 
            sltTask.Location = new Point(437, 12);
            sltTask.Name = "sltTask";
            sltTask.Size = new Size(114, 40);
            sltTask.TabIndex = 7;
            // 
            // label1
            // 
            label1.Location = new Point(356, 21);
            label1.Name = "label1";
            label1.Size = new Size(75, 23);
            label1.TabIndex = 8;
            label1.Text = "Số luồng";
            // 
            // pnlControl
            // 
            pnlControl.Back = SystemColors.Control;
            pnlControl.BackColor = SystemColors.Control;
            pnlControl.Controls.Add(label1);
            pnlControl.Controls.Add(sltTask);
            pnlControl.Controls.Add(chkNoVie);
            pnlControl.Controls.Add(btnPathSave);
            pnlControl.Controls.Add(btnStart);
            pnlControl.Controls.Add(txtPathSave);
            pnlControl.Controls.Add(txtPath);
            pnlControl.Controls.Add(btnGetPath);
            pnlControl.Dock = DockStyle.Top;
            pnlControl.Location = new Point(0, 0);
            pnlControl.Name = "pnlControl";
            pnlControl.Size = new Size(565, 167);
            pnlControl.TabIndex = 8;
            pnlControl.Text = "panel1";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 251);
            Controls.Add(pnlProgress);
            Controls.Add(pnlControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Audio Converter";
            Load += Form1_Load;
            pnlProgress.ResumeLayout(false);
            pnlControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Progress progressing;
        private OpenFileDialog openFileDialog1;
        private AntdUI.Button btnEnd;
        private AntdUI.Panel pnlProgress;
        private AntdUI.Button btnGetPath;
        private AntdUI.Input txtPath;
        private AntdUI.Input txtPathSave;
        private AntdUI.Button btnStart;
        private AntdUI.Button btnPathSave;
        private AntdUI.Checkbox chkNoVie;
        private AntdUI.Select sltTask;
        private AntdUI.Label label1;
        private AntdUI.Panel pnlControl;
    }
}
