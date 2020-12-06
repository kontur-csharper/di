using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordCloudGenerator
{
    public class RandomChoicePalette : IPalette
    {
        private readonly Color[] colors;

        public RandomChoicePalette(IEnumerable<Color> colors, Color bgColor)
        {
            this.colors = colors.ToArray();
            BackgroundColor = bgColor;
        }

        public Color BackgroundColor { get; }

        public Color GetNextColor()
        {
            var rnd = new Random();
            return colors[rnd.Next(0, colors.Length)];
        }
    }
}