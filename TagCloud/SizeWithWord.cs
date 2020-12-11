using System.Drawing;

namespace TagCloud
{
    public class SizeWithWord
    {
        public Size Size { get; }
        public Word Word { get; }

        public SizeWithWord(Size size, Word word)
        {
            Size = size;
            Word = word;
        }
    }
}