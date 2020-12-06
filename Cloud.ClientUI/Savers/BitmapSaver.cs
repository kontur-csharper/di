using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Cloud.ClientUI
{
    public class BitmapSaver : ISaver
    {
        public void SaveImage(Bitmap bitmap, string fileName)
        {
            CheckValidFileName(fileName);
            var path = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{fileName}.png";
            bitmap.Save(path, ImageFormat.Bmp);
        }

        private static void CheckValidFileName(string name)
        {
            var invalidSpecialCharacters = "/\\:*?\"<>|".ToCharArray();
            if (name.Any(letter => invalidSpecialCharacters.Contains(letter)))
                throw new ArgumentException("File name contains invalid characters");
        }
    }
}