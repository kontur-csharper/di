using System.Drawing;

namespace TagCloud
{
    public class RectangleWithWord
    {
        public Rectangle Rectangle { get; }
        public Word Word { get; }

        public RectangleWithWord(Rectangle rect, Word word)
        {
            Rectangle = rect;
            Word = word;
        }
    }
}