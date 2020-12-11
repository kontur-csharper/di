using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.ImageSavers
{
    public class BmpSaver : IImageSaver
    {
        public HashSet<string> Extensions { get; } = new HashSet<string> {".bmp"};

        public void Save(Image image, string fileName) => image.Save(fileName, ImageFormat.Bmp);
    }
}