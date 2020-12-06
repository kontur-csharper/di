using System.Drawing;

namespace WordCloudGenerator
{
    public interface ILayouter
    {
        public RectangleF PutNextRectangle(SizeF rectSize);

        public RectangleF[] GetRectangles();
    }
}