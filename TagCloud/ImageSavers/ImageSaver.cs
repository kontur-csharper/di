using System;
using System.Drawing;
using System.IO;
using TagCloud.Settings;

namespace TagCloud.ImageSavers
{
    public class ImageSaver : IImageSaver
    {
        private readonly string path;
        private readonly string fileName;
        private readonly string extension;

        public ImageSaver(SaverSettings settings)
        {
            path = settings.Path ?? Directory.GetCurrentDirectory();
            fileName = settings.FileName;
            extension = settings.Extension;
        }

        public void Save(Bitmap bitmap)
        {
            var fullPath = $"{path}/{fileName}.{extension}";
            bitmap.Save(fullPath);
            Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }
    }
}