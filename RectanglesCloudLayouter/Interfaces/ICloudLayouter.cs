using System.Drawing;

namespace RectanglesCloudLayouter.Interfaces
{
    public interface ICloudLayouter
    {
        int CloudRadius { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}