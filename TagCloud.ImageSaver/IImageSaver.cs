using System.Drawing;
using TagCloud.Visualizer;

namespace TagCloud.ImageSaver
{
    public interface IImageSaver
    {
        public bool TrySaveImage(Bitmap bitmap, string savePath, ImageOptions imageOptions);
    }
}