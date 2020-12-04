using System.Drawing;

namespace TagCloud
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly ITagCloud tagCloud;

        public TagCloudVisualizer(ITagCloud tagCloud)
        {
            this.tagCloud = tagCloud;
        }

        public Bitmap CreateBitMap(int width, int height, Color[] colors, string fontFamily)
        {
            var bitMap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitMap);
            graphics.Clear(Color.Black);
            var colorNumber = 0;

            foreach (var wordRectangle in tagCloud.WordRectangles)
            {
                var color = colors[colorNumber];
                var word = wordRectangle.Word;
                var rectangle = wordRectangle.Rectangle;
                var font = GetBiggestFont(wordRectangle, fontFamily, graphics);
                graphics.DrawString(word, font, new SolidBrush(color),
                    new PointF(rectangle.Left, rectangle.Y));
                graphics.DrawPolygon(new Pen(color), RectangleToPointFArray(rectangle));

                colorNumber++;
                colorNumber %= colors.Length;
            }

            return bitMap;
        }

        private Font GetBiggestFont(WordRectangle token, string fontFamilyName, Graphics graphics)
        {
            var word = token.Word;
            var rectangle = token.Rectangle;
            var fontSize = 10;
            for (; fontSize < 100; fontSize++)
            {
                var font = new Font(fontFamilyName, fontSize);
                var size = graphics.MeasureString(word, font);
                if (size.Height > rectangle.Height || size.Width > rectangle.Width)
                    break;
            }

            return new Font(fontFamilyName, fontSize - 1);
        }

        private static PointF[] RectangleToPointFArray(Rectangle rectangle)
        {
            return new[]
            {
                new PointF(rectangle.Left, rectangle.Bottom),
                new PointF(rectangle.Right, rectangle.Bottom),
                new PointF(rectangle.Right, rectangle.Top),
                new PointF(rectangle.Left, rectangle.Top)
            };
        }
    }
}