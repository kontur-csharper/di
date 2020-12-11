using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Visualizer;

namespace TagCloud.ImageSaver
{
    public class CompositeImageSaver : IImageSaver
    {
        private readonly IEnumerable<IImageSaver> implementations;

        public CompositeImageSaver(IEnumerable<IImageSaver> implementations)
        {
            this.implementations = implementations;
        }

        public bool TrySaveImage(Bitmap bitmap, string savePath, ImageOptions imageOptions)
        {
            return implementations.Any(imageSaver => imageSaver.TrySaveImage(bitmap, savePath, imageOptions));
        }
    }
}