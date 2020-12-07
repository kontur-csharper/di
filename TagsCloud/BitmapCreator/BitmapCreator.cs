using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Extensions;
using TagsCloud.ImageConfig;
using TagsCloud.LayoutAlgorithms;

namespace TagsCloud.BitmapCreator
{
    public class BitmapCreator : IBitmapCreator
    {
        private readonly ILayoutAlgorithm _algorithm;
        private readonly IImageConfig _imageConfig;
        private Bitmap _bitmap;
        private Graphics _graphics;

        public BitmapCreator(ILayoutAlgorithm algorithm, IImageConfig imageConfig)
        {
            _algorithm = algorithm;
            _imageConfig = imageConfig;
        }

        public Bitmap Create(IReadOnlyCollection<string> words)
        {
            var width = _imageConfig.Size.Width;
            var height = _imageConfig.Size.Height;

            _bitmap = new Bitmap(width, height);
            _graphics = Graphics.FromImage(_bitmap);

            _graphics.FillRectangle(new SolidBrush(_imageConfig.BackgroundColor), 0, 0, width, height);

            if (words.Count <= 0)
                return _bitmap;

            DrawWords(words, width, height);

            return _bitmap;
        }

        private void DrawWords(IReadOnlyCollection<string> words, int width, int height)
        {
            var frequency = words.GetFrequency().OrderByDescending(x => x.Value).ToArray();

            var minFontSize = width / 100;
            var maxFontSize = width / 30;
            var maxFreq = frequency.Max(x => x.Value);

            foreach (var (word, freq) in frequency)
            {
                var font = GetFont(minFontSize, maxFontSize, freq, maxFreq);
                var rectangle = GetWordRectangle(font, word);
                var brush = new SolidBrush(_imageConfig.ColoringAlgorithm.GetNextColor());

                _graphics.DrawString(word, font, brush, rectangle);
            }
        }

        private Font GetFont(int minFontSize, int maxFontSize, double currentFreq, double maxFreq)
        {
            var fontSize = (int) Math.Ceiling(currentFreq / maxFreq * (maxFontSize - minFontSize) + minFontSize);
            var font = new Font(_imageConfig.FontFamily, fontSize);
            return font;
        }

        private Rectangle GetWordRectangle(Font font, string word)
        {
            var stringSize = _graphics.MeasureString(word, font);
            var rectWidth = (int) Math.Ceiling(stringSize.Width) + 3;
            var rectHeight = (int) Math.Ceiling(stringSize.Height) + 3;
            var rectangle = _algorithm.PutNextRectangle(new Size(rectWidth, rectHeight));
            return rectangle;
        }

        public void Dispose()
        {
            _graphics.Dispose();
            _bitmap.Dispose();
        }
    }
}
