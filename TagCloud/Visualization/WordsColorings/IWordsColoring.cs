using System.Drawing;

namespace TagCloud.Visualization.WordsColorings
{
    internal interface IWordsColoring
    {
        public Color GetColor(string word, Rectangle location, TagCloud cloud);
    }
}
