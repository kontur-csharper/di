using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace WordCloudGenerator
{
    public abstract class Saver
    {
        private static string GetFormatStr(string path)
        {
            var index = path.LastIndexOf('.');
            return path.Substring(index + 1);
        }

        public static bool IsPathCorrect(string path)
        {
            var format = GetFormatStr(path);

            return format == "png" || format == "jpg" || format == "bmp" || format == "jpeg";
        }

        public static void SaveImage(Image img, string path)
        {
            ImageFormat format;
            switch (GetFormatStr(path))
            {
                case "png":
                    format = ImageFormat.Png;
                    break;
                case "bmp":
                    format = ImageFormat.Bmp;
                    break;
                case "jpg":
                case "jpeg":
                    format = ImageFormat.Jpeg;
                    break;

                default: throw new ArgumentException("Некорректный путь к файлу");
            }

            img.Save(path, format);
        }
    }
}