using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Core.Layouting
{
    public interface ILayouter
    {
        LayouterType Type { get; }

        IEnumerable<Rectangle> PutAll(Point centerPoint,
            Size betweenRectanglesDistance,
            IEnumerable<Size> words);
    }
}