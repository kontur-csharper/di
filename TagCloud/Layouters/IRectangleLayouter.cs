using System.Drawing;

namespace TagCloud.Layouters
{
    public interface IRectangleLayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}