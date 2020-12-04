using System.Drawing;

namespace TagCloud.ImageSavers
{
    public interface IImageSaver
    {
        public void Save(Bitmap bitmap);
    }
}