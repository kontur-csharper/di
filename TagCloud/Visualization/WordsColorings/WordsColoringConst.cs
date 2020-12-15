using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.Visualization.WordsColorings
{
    internal class WordsColoringConst : IWordsColoring
    {
        private readonly Color color;

        internal WordsColoringConst(Color color) => this.color = color;
        public Color GetColor(string word, Rectangle location, TagCloud cloud) => color;
    }
}
