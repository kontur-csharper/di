using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace TagsCloud
{
    public static class ArgumentParser
    {
        public static string CheckFilePath(string filePath)
        {
            if (!File.Exists(filePath))
                ErrorHandler("ArgumentException: введенного вами файла не существует. Для подробностей --help");

            if (!filePath.EndsWith(".txt") && !filePath.EndsWith("docx"))
                ErrorHandler("ArgumentException: расширения данного файла не поддерживаются. Для подробностей --help");

            return filePath;
        }

        public static Color GetColor(string colorName)
        {
            try
            {
                return Color.FromName(colorName);
            }
            catch (Exception)
            {
                ErrorHandler(
                    "ArgumentException. Аргумент -t: введенного вами цвета не существует. Для подробностей --help");
                return default;
            }
        }

        public static FontFamily GetFontFamily(string fontFamilyName)
        {
            try
            {
                return new FontFamily(fontFamilyName);
            }
            catch (Exception)
            {
                ErrorHandler(
                    "ArgumentException. Аргумент -f: введенного вами шрифта не сущесвует. Для подробностей --help");
                return default;
            }
        }

        public static bool IsCorrectFormat(string format)
        {
            if (format == "png" || format == "jpg" || format == "jpeg" || format == "bmp")
                return true;

            ErrorHandler(
                "ArgumentException. Аргумент -r: введенный вами формат не поддерживается. Для подробностей --help");
            return default;
        }

        public static Size GetSize(string size)
        {
            var widthAndHeight = size.Split('x');

            Size result = default;
            if (int.TryParse(widthAndHeight[0], out var width) && int.TryParse(widthAndHeight[1], out var height) &&
                (width > 0 && height > 0))
                result = new Size(width, height);
            else
                ErrorHandler(
                    "ArgumentException. Аргумент -s: введенный вами размер некоректен. Для подробностей --help");
            return result;
        }

        private static void ErrorHandler(string text)
        {
            Console.WriteLine(text);
            Process.GetCurrentProcess().Kill();
        }
    }
}
