using System.Drawing;

namespace TagCloud
{
    public class WordForCloud
    {
        public Font Font { get; }
        public Rectangle WordSize { get; }

        public string Word { get; }
        public Color WordColor { get; }

        public WordForCloud(string font, int fontSize, string word, Rectangle wordSize, Color wordColor)
        {
            Font = new Font(font, fontSize);
            WordSize = wordSize;
            WordColor = wordColor;
            Word = word;
        }
    }
}