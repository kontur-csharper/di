using System;
using System.Drawing;

namespace TagCloud.Core.Text.Formatting
{
    public class FormattedWord : IDisposable
    {
        public FormattedWord(string word, Font font, Brush brush)
        {
            Word = word;
            Font = font;
            Brush = brush;
        }

        public string Word { get; }
        public Font Font { get; }
        public Brush Brush { get; }

        public void Dispose()
        {
            Font.Dispose();
            Brush.Dispose();
        }
    }
}