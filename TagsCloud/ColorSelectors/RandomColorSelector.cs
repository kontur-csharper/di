using System;
using System.Drawing;

namespace TagsCloud.ColorSelectors
{
    public class RandomColorSelector : IColorSelector
    {
        private readonly Random random = new Random();
        private readonly Color[] colors;

        public RandomColorSelector(Color[] colors)
        {
            if(colors.Length == 0) throw new ArgumentException("Empty colors");
            this.colors = colors;
        }

        public Color Next()
        {
            return colors[random.Next(colors.Length - 1)];
        }
    }
}