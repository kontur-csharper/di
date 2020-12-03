using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.ColoringAlgorithms;

namespace TagCloud.Core.ImageCreators
{
    public interface IImageCreator
    {
        Bitmap Create(IColoringAlgorithm algorithm, IEnumerable<Tag> tags,
            string fontName, Size size);
    }
}