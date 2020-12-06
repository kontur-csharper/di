using System.Collections.Generic;
using System.Drawing;

namespace WordCloudGenerator
{
    public interface IPalette
    {
        public delegate IPalette Factory(IEnumerable<Color> colors, Color bgColor);
        public Color BackgroundColor { get; }
        public Color GetNextColor();
    }
}