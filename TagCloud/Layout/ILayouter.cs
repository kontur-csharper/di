using System.Drawing;

namespace TagCloud.Layout
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}