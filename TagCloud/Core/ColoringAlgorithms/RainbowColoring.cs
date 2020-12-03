using System;
using System.Drawing;

namespace TagCloud.Core.ColoringAlgorithms
{
    public class RainbowColoring : IColoringAlgorithm
    {
        private readonly Color[] colors;
        private readonly Random randomizer;
        public ColoringTheme Theme => ColoringTheme.Rainbow;

        public RainbowColoring()
        {
            randomizer = new Random();
            colors = new[]
            {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.ForestGreen,
                Color.DeepSkyBlue,
                Color.Blue,
                Color.DarkViolet
            };
        }

        public Color GetNextColor(Tag tag)
        {
            var randomNumber = randomizer.Next(colors.Length);
            return colors[randomNumber];
        }
    }
}