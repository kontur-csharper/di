using System.Drawing;

namespace TagCloud
{
    public class Canvas : ICanvas
    {
        public Point Center { get; }
        public int Width { get; }
        public int Height { get; }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            Center = new Point(width / 2, height / 2);
        }
    }
}