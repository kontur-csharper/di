using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.ImageSavers
{
    public class JpgSaver : IImageSaver
    {
        public HashSet<string> Extensions { get; } = new HashSet<string> {".jpg", ".jpeg"};

        public void Save(Image image, string fileName) => image.Save(fileName, ImageFormat.Jpeg);
    }
}