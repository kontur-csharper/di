using System.Drawing;

namespace RectanglesCloudLayouter.Interfaces
{
    public interface ICloudRadiusCalculator
    {
        int CloudRadius { get; }
        void UpdateCloudRadius(Point spiralCenter, Rectangle currentRectangle);
    }
}