using System;
using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordMeasurer : IWordMeasurer
    {
        public Size GetWordSize(string word, Font font)
        {
            if (word == null || font == null)
                throw new ArgumentException("String and font must be not null");
            var sizeF = Graphics.FromHwnd(IntPtr.Zero).MeasureString(word, font);
            return new Size((int) Math.Ceiling(sizeF.Width), (int) Math.Ceiling(sizeF.Height));
        }
    }
}