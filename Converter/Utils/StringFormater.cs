using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Converter.Utils
{
    public static class StringFormater
    {
        public static string GenerateUniqueFileName() => DateTime.Now.ToString("yyyyMMddHHmmssfff");

        public static string ConvertToSlug(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // B1: Loại bỏ dấu tiếng Việt
            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            string noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC);

            // B2: Loại bỏ dấu câu (ký tự đặc biệt không phải chữ, số, khoảng trắng)
            string noPunctuation = Regex.Replace(noDiacritics, @"[^\w\s]", "");

            // B3: Thay thế khoảng trắng bằng dấu _
            string result = Regex.Replace(noPunctuation, @"\s+", "_");

            // B4: Chuyển về chữ thường (tùy chọn)
            return result.ToLower();
        }
    }
}
