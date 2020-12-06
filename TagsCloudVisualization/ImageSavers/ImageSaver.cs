using System.Drawing.Imaging;
using System.IO;
using TagsCloudVisualization.Canvases;

namespace TagsCloudVisualization.ImageSavers
{
    public class ImageSaver : IImageSaver
    {
        public void SaveImage(ICanvas canvas, string fileName)
        {
            canvas.SaveImage(fileName, GetSaveImageFormat(fileName));
        }

        private static ImageFormat GetSaveImageFormat(string fileName)
        {
            return Path.GetExtension(fileName) switch
            {
                ".BMP" => ImageFormat.Bmp,
                ".Jpg" => ImageFormat.Jpeg,
                ".Gif" => ImageFormat.Gif,
                _ => ImageFormat.Png
            };
        }
    }
}