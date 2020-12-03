using System.Drawing;
using TagCloud.Core.LayoutAlgorithms;

namespace TagCloudUI.Infrastructure
{
    public class SpiralFactory : ISpiralFactory
    {
        private readonly IAppSettings settings;

        public SpiralFactory(IAppSettings settings)
        {
            this.settings = settings;
        }

        public ISpiral Create()
        {
            var center = new Point(settings.ImageWidth / 2, settings.ImageHeight / 2);
            return new ArchimedeanSpiral(center);
        }
    }
}