using System.Drawing;
using TagsCloud.Common;
using TagsCloud.Visualization;

namespace TagsCloud.Spirals
{
    public class SpiralFactory : ISpiralFactory
    {
        private readonly IImageHolder imageHolder;
        private readonly SpiralSettings settings;

        public SpiralFactory(IImageHolder imageHolder, SpiralSettings settings)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
        }

        public ISpiral Create()
        {
            var size = imageHolder.GetImageSize();
            return new ArchimedeanSpiral(new Point(size.Width / 2, size.Height / 2), settings.SpiralParameter);
        }
    }
}
