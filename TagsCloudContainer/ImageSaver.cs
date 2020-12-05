using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer
{
    public class ImageSaver : IImageSaver
    {
        private readonly string imagePath;
        private ImageFormat format;

        public ImageSaver(string imagePath)
        {
            this.imagePath = imagePath;
            format = GetImageFormat(Path.GetExtension(imagePath));
        }
        
        public void Save(Bitmap image)
        {
            image.Save(imagePath, format);
        }

        private static ImageFormat GetImageFormat(string fileExt)
        {
            return fileExt switch
            {
                var ext when ext == ".jpeg" || ext == ".jpg" => ImageFormat.Jpeg,
                var ext when ext == ".tiff" || ext == ".tif" => ImageFormat.Tiff,
                ".png" => ImageFormat.Png,
                ".gif" => ImageFormat.Gif,
                ".bmp" => ImageFormat.Bmp,
                _ => throw new ArgumentException($"This format of image is not supported {fileExt}")
            };
        }
    }
}