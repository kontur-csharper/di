using System.Drawing;

namespace TagsCloudContainer
{
    public class LayoutedWord
    {
        public readonly string Word;
        public readonly int Count;
        public RectangleF Rectangle;

        public LayoutedWord(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public LayoutedWord(string word, int count, SizeF size) : this(word, count)
        {
            Rectangle = new RectangleF(PointF.Empty, size);
        }

        public LayoutedWord(string word, int count, RectangleF rectangle) : this(word, count)
        {
            Rectangle = rectangle;
        }
    }
}