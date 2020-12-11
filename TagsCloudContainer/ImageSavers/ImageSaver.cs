using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer
{
    abstract class ImageSaver : IImageSaver
    {
        public string FormatName { get; set; }
        public ImageFormat Format { get; set; }

        public void Save(string path, string name, Bitmap bitmap)
        {
            bitmap.Save(path + "\\" + name + "." + FormatName, Format);
        }
    }
}
