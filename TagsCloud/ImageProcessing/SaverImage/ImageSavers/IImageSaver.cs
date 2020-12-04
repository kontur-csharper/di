using System.Drawing;

namespace TagsCloud.ImageProcessing.SaverImage.ImageSavers
{
    public interface IImageSaver
    {
        void SaveImage(Bitmap bitmap, string path);
        bool CanSave(string path);
    }
}
