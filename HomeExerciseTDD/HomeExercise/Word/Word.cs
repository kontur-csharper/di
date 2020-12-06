using System.Drawing;

namespace HomeExercise
{
    public class Word : IWord
    {
        public string Text { get; }
        public int Frequency { get; }
        public FontFamily Font { get; }
        public int Size { get; }

        public Word(string text, int frequency, FontFamily font, int size)
        {
            Text = text;
            Frequency = frequency;
            Font = font;
            Size = size;
        }
    }
}