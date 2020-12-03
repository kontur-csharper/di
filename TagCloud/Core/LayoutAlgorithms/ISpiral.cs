using System.Drawing;

namespace TagCloud.Core.LayoutAlgorithms
{
    public interface ISpiral
    {
        Point Start { get; }
        PointF GetNextPoint();
    }
}