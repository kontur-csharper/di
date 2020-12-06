using System.Drawing;

namespace HomeExercise
{
    public interface IWord
    {
        public string Text { get; }
        public FontFamily Font { get; }
        public int Size { get; }
    }
}