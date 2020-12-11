using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        internal readonly List<Rectangle> Rectangles = new List<Rectangle>();
        private readonly Spiral spiral;

        public CircularCloudLayouter(CenterOptions options)
        {
            spiral = new Spiral(new Point(options.X, options.Y));
        }

        public List<RectangleWithWord> GetRectangles(IEnumerable<SizeWithWord> sizes)
        {
            return sizes.Select(PutNextRectangle).ToList();
        }

        private RectangleWithWord PutNextRectangle(SizeWithWord rectangleSizeWithWord)
        {
            var newRect = FindPlaceForRect(rectangleSizeWithWord.Size);
            Rectangles.Add(newRect);
            return new RectangleWithWord(newRect, rectangleSizeWithWord.Word);
        }

        private Rectangle FindPlaceForRect(Size rectangleSize)
        {
            var resultRect = new Rectangle(spiral.GetNextPoint(), rectangleSize);
            while (resultRect.IntersectsWith(Rectangles))
            {
                resultRect = new Rectangle(spiral.GetNextPoint(), rectangleSize);
            }

            return resultRect;
        }
    }
}