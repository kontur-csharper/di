using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;

namespace TagCloud.Core.LayoutAlgorithms
{
    public class CircularCloudLayouter : ILayoutAlgorithm
    {
        private readonly Point center;
        private readonly ISpiral spiral;
        private readonly List<Rectangle> rectangles;

        public IEnumerable<Rectangle> Rectangles => rectangles;
        public LayoutAlgorithmType Type => LayoutAlgorithmType.Circular;


        public CircularCloudLayouter(ISpiral spiral)
        {
            center = spiral.Start;
            this.spiral = spiral;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rectangle;
            do
            {
                var nextPoint = Point.Round(spiral.GetNextPoint());
                rectangle = new Rectangle(nextPoint, rectangleSize);
            } while (IsIntersectsWithLayout(rectangle));

            if (rectangles.Count > 0)
                rectangle = ShiftToCenter(rectangle);

            rectangles.Add(rectangle);
            return rectangle;
        }

        public Size GetLayoutSize()
        {
            if (rectangles.Count == 0)
                throw new ArgumentException("Layout is empty");

            var maxDeltaFromCenter = center.GetMaxDistanceToLayoutBorder(rectangles);
            var width = 2 * Math.Max(center.X, maxDeltaFromCenter.X);
            var height = 2 * Math.Max(center.Y, maxDeltaFromCenter.Y);

            return new Size(width, height);
        }

        private Rectangle ShiftToCenter(Rectangle rect)
        {
            var offsetX = center.X - rect.X < 0 ? -1 : 1;
            var offsetY = center.Y - rect.Y < 0 ? -1 : 1;

            rect = ShiftThroughDirection(rect, new Point(offsetX, 0));
            return ShiftThroughDirection(rect, new Point(0, offsetY));
        }

        private Rectangle ShiftThroughDirection(Rectangle rect, Point direction)
        {
            var shiftedRect = rect;

            while (!IsIntersectsWithLayout(rect) &&
                   rect.X != center.X && rect.Y != center.Y)
            {
                shiftedRect = rect;
                var newLocation = new Point(
                    rect.Location.X + direction.X,
                    rect.Location.Y + direction.Y);
                rect = new Rectangle(newLocation, rect.Size);
            }

            return shiftedRect;
        }

        private bool IsIntersectsWithLayout(Rectangle rectangle)
        {
            return rectangles.Any(rectangle.IntersectsWith);
        }
    }
}