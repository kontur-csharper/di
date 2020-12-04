using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.ColorSelectors;
using TagsCloud.PointsLayouts;

namespace TagsCloud.WordLayouters
{
    public class WordLayouter : IWordLayouter
    {
        public List<CloudWord> CloudWords { get; } = new List<CloudWord>();
        
        private readonly IPointsLayout pointsLayout;
        private readonly IColorSelector colorSelector;
        private readonly FontFamily family;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        private int Top;
        private int Left;
        private int Bottom;
        private int Right;

        public WordLayouter(FontFamily family, IPointsLayout pointsLayout, IColorSelector colorSelector)
        {
            this.family = family;
            this.pointsLayout = pointsLayout;
            this.colorSelector = colorSelector;
        }

        public void AddWords(Dictionary<string, int> statistic)
        {
            foreach (var word in statistic.OrderByDescending(s => s.Value))
            {
                var font = new Font(family, word.Value * 15);
                var size = TextRenderer.MeasureText(word.Key, font);
                var rectangle = PutNextRectangle(size);

                CloudWords.Add(new CloudWord(word.Key, rectangle, font, colorSelector.Next()));
            }
        }
        
        public Rectangle GetCloudRectangle() => new Rectangle(Left, Top, Right - Left, Bottom - Top);

        private Rectangle PutNextRectangle(Size size)
        {
            var rectangle = GetNextRectangle(size);
            FitImageBorders(rectangle);
            rectangles.Add(rectangle);
            return rectangle;
        }
        
        private bool HaveIntersection(Rectangle rectangle) => rectangles.Any(other => other.IntersectsWith(rectangle));

        private void FitImageBorders(Rectangle rectangle)
        {
            if (rectangle.Top < Top) Top = rectangle.Top;
            if (rectangle.Left < Left) Left = rectangle.Left;
            if (rectangle.Bottom > Bottom) Bottom = rectangle.Bottom;
            if (rectangle.Right > Right) Right = rectangle.Right;
        }
        
        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            var halfSize = new Size(rectangleSize.Width / 2, rectangleSize.Height / 2);
            using var points = pointsLayout.GetPoints().GetEnumerator();
            points.MoveNext();
            var rectangle = new Rectangle(points.Current - halfSize, rectangleSize); 
            while (HaveIntersection(rectangle))
            {
                points.MoveNext();
                rectangle.Location = points.Current - halfSize;
            }

            return rectangle;
        }
    }
}