using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordCloudGenerator
{
    public class OrderedChoicePalette : IPalette
    {
        private readonly Color[] colors;

        private int colorIndex = -1;

        public OrderedChoicePalette(IEnumerable<Color> colors, Color bgColor)
        {
            this.colors = colors.ToArray();
            BackgroundColor = bgColor;
        }

        public Color BackgroundColor { get; }

        public Color GetNextColor()
        {
            colorIndex = (colorIndex + 1) % colors.Length;
            return colors[colorIndex];
        }
    }
}