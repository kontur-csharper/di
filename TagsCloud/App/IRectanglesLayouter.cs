using System.Drawing;

namespace TagsCloud.App
{
    public interface IRectanglesLayouter
    {
        string Name { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
        void Reset();
    }
}