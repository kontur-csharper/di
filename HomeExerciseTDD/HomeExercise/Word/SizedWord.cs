using System.Drawing;

namespace HomeExercise
{
    public class SizedWord : ISizedWord
    {
        public string Text { get; }
        public FontFamily Font { get; }
        public Rectangle Rectangle { get; }
        public int Size { get; }

        public SizedWord(IWord word, int size, FontFamily font, Rectangle rectangle)
        {
            Text = word.Text;
            Font = font;
            Rectangle = rectangle;
            Size = size;
        }
    }
}