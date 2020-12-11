using System.Drawing;
using System.IO;
using TagCloud.Visualizer;

namespace TagCloud.ImageSaver
{
    public class PngSaver : IImageSaver
    {
        public bool TrySaveImage(Bitmap bitmap, string savePath, ImageOptions imageOptions)
        {
            if (Path.GetExtension(imageOptions.ImageSaveName) != ".png")
            {
                return false;
            }

            bitmap.Save(Path.Combine(savePath, $"{imageOptions.ImageSaveName}"));
            return true;
        }
    }
}