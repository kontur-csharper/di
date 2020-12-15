using System;
using System.Drawing;

namespace TagCloud.Visualization.WordsColorings
{
    internal class WordsColoringRandom : IWordsColoring
    {
        private readonly Random random = new Random();
        public Color GetColor(string word, Rectangle location, TagCloud cloud) =>
            Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255));
    }
}
