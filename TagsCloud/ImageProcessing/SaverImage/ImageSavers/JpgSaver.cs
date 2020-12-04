using System.Drawing.Imaging;

namespace TagsCloud.ImageProcessing.SaverImage.ImageSavers
{
    public class JpgSaver : SaverBase
    {
        public override ImageFormat ImageFormat => ImageFormat.Jpeg;

        public override bool CanSave(string path) => path.EndsWith(".jpg");
    }
}
