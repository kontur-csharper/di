using System.Drawing.Imaging;

namespace TagsCloud.ImageProcessing.SaverImage.ImageSavers
{

    public class PngSaver : SaverBase
    {
        public override ImageFormat ImageFormat => ImageFormat.Png;

        public override bool CanSave(string path) => path.EndsWith(".png");
    }
}
