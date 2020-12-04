using System.Drawing;

namespace TagCloud
{
    public interface IVisualizer
    {
        string Visualize(string filename, FontFamily fontFamily, Color stringColor);
    }
}