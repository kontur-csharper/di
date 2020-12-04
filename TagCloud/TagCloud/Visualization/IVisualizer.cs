using System.Drawing;

namespace TagCloud
{
    public interface IVisualizer
    {
        Bitmap CreateBitMap(int width, int height, Color[] colors, string fontFamily);
    }
}