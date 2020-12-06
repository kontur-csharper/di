using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordCloudGenerator
{
    public class Painter : IPainter
    {
        private readonly FontFamily fontFamily;
        private readonly ILayouter layouter;
        private readonly IPalette palette;
        private Size imgSize;

        public Painter(ILayouter layouter, FontFamily fontFamily, IPalette palette,
            Size imgSize = default)
        {
            this.layouter = layouter;
            this.fontFamily = fontFamily;
            this.palette = palette;
            this.imgSize = imgSize;
        }

        public Bitmap Paint(IEnumerable<GraphicString> words)
        {
            var wordsArr = words.ToArray();
            FillLayouter(wordsArr);
            var layoutRects = layouter.GetRectangles();

            if (imgSize == default)
                imgSize = CalculateImageSize(layoutRects);

            var bitmap = new Bitmap(imgSize.Width, imgSize.Height);
            using var g = Graphics.FromImage(bitmap);
            
            var rectIndex = 0;
            var shiftVector = CalculateShiftVector(layoutRects);
            var background = new Rectangle{Size = imgSize};
            g.DrawRectangle(new Pen(palette.BackgroundColor), background );
            g.FillRectangle(new SolidBrush(palette.BackgroundColor), background);
            foreach (var word in wordsArr)
            {
                var font = new Font(fontFamily, word.FontSize);
                using var brush = new SolidBrush(palette.GetNextColor());
                g.DrawString(word.Value, font, brush, layoutRects[rectIndex].Shift(shiftVector));
                
                rectIndex++;
            }

            return bitmap;
        }

        private void FillLayouter(IEnumerable<GraphicString> words)
        {
            using var gForMeasure = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var word in words)
            {
                var font = new Font(fontFamily, word.FontSize);
                var size = gForMeasure.MeasureString(word.Value, font);
                layouter.PutNextRectangle(size);
            }
        }

        private static Size CalculateImageSize(RectangleF[] rectangles)
        {
            var maxX = (int) rectangles.Max(rect => rect.X + rect.Width);
            var minX = (int) rectangles.Min(rect => rect.X);
            var maxY = (int) rectangles.Max(rect => rect.Y + rect.Height);
            var minY = (int) rectangles.Min(rect => rect.Y);

            var width = maxX - minX;
            var height = maxY - minY;

            return new Size(width, height);
        }

        private static Point CalculateShiftVector(RectangleF[] rectangles)
        {
            var minX = (int) rectangles.Min(rect => rect.X);
            var minY = (int) rectangles.Min(rect => rect.Y);

            return new Point(-minX, -minY);
        }
    }
}