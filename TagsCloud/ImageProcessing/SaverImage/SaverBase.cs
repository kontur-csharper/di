using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;

namespace TagsCloud.ImageProcessing.SaverImage
{
    public abstract class SaverBase : IImageSaver
    {
        public abstract ImageFormat ImageFormat { get; }

        public abstract bool CanSave(string path);

        public void SaveImage(Bitmap bitmap, string path)
        {
            bitmap.Save(path, ImageFormat);
        }
    }
}
