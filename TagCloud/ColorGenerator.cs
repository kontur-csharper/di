using System.Collections.Generic;
using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class ColorGenerator : IColorGenerator
    {
        private readonly Color[] colors;
        private int index = 0;

        public ColorGenerator(Color[] colors)
        {
            this.colors = colors;
        }

        public Color GetNextColor()
        {
            index++;
            if (index >= colors.Length)
                index = 0;
            return colors[index];
        }
    }
}