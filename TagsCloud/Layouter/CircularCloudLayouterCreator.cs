using System.Drawing;
using TagsCloud.ImageProcessing.Config;

namespace TagsCloud.Layouter
{
    public class CircularCloudLayouterCreator
    {
        private readonly ImageConfig imageConfig;

        public CircularCloudLayouterCreator(ImageConfig imageConfig)
        {
            this.imageConfig = imageConfig;
        }

        public CircularCloudLayouter Create()
        {
            var center = new Point(imageConfig.ImageSize.Width / 2, imageConfig.ImageSize.Height / 2);
            return new CircularCloudLayouter(center);
        }
    }
}
