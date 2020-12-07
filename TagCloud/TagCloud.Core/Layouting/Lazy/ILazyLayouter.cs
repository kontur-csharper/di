using System.Drawing;

namespace TagCloud.Core.Layouting.Lazy
{
    public interface ILazyLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}