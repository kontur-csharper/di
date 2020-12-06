using System.Drawing;

namespace RectanglesCloudLayouter.Interfaces
{
    public interface ISpiral
    {
        Point Center { get; }
        Point GetNewSpiralPoint();
    }
}