using System;
using System.Drawing;

namespace TagsCloud.ColoringAlgorithms
{
    public class RandomColoringAlgorithm : IColoringAlgorithm
    {
        private readonly Random random = new Random();
        public Color GetNextColor()
        {
            return Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        }
    }
}
