using System.Drawing.Imaging;

namespace TagsCloud.ImageProcessing.SaverImage.ImageSavers
{
    public class BmpSaver : SaverBase
    {
        public override ImageFormat ImageFormat => ImageFormat.Bmp;

        public override bool CanSave(string path) => path.EndsWith(".bmp");
    }
}
