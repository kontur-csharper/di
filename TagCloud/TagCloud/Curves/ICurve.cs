using System.Drawing;

namespace TagCloud.Curves
{
    public interface ICurve
    {
        Point CurrentPoint { get; }
        void Next();
    }
}