using System.Drawing;

namespace TagCloud.Core.LayoutAlgorithms
{
    public interface ILayoutAlgorithm
    {
        LayoutAlgorithmType Type { get; }
        Rectangle PutNextRectangle(Size rectSize);
        Size GetLayoutSize();
    }
}