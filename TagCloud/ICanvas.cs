using System.Drawing;

namespace TagCloud
{
    public interface ICanvas
    {
        Point Center { get; }
        int Width { get; }
        int Height { get; }
    }
}