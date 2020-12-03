using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Layouter
{
    public class CircularCloudLayouter : ILayouter
    {
        private Point center;
        private readonly List<Rectangle> rectangles;
        private readonly List<Direction> directions;

        public CircularCloudLayouter()
        {
            center = new Point();
            rectangles = new List<Rectangle>();
            directions = new List<Direction> {Direction.Top, Direction.Right, Direction.Bottom, Direction.Left};
        }

        public void SetCenter(Point center)
        {
            this.center = center;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Rectangle size should be positive.");
            Rectangle rectangle;
            if (rectangles.Count == 0)
            {
                rectangle = new Rectangle
                {
                    Size = rectangleSize,
                    Location = new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2)
                };
            }
            else
                rectangle = GetRectangle(rectangleSize);

            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetRectangle(Size rectangleSize)
        {
            var bestDistance = double.MaxValue;
            var bestRectangle = new Rectangle();

            foreach (var rectangle in rectangles)
            {
                foreach (var direction in directions)
                {
                    var newRectangle = new Rectangle
                    {
                        Size = rectangleSize,
                        Location = CalculateRectangleLocation(rectangle, rectangleSize, direction)
                    };
                    var distance = GetDistanceToCenter(newRectangle);

                    if (distance < bestDistance && !IntersectsWithOtherRectangles(newRectangle))
                    {
                        bestDistance = distance;
                        bestRectangle = newRectangle;
                    }
                }
            }

            return bestRectangle;
        }

        private double GetDistanceToCenter(Rectangle rectangle)
        {
            var rectangleCenter = new Point(rectangle.X + rectangle.Width / 2,
                rectangle.Y + rectangle.Height / 2);
            return LayouterTools.CalculateDistance(rectangleCenter, center);
        }

        private bool IntersectsWithOtherRectangles(Rectangle rectangle)
        {
            return rectangles.Any(previous => previous.IntersectsWith(rectangle));
        }

        private static Point GetOffset(Size rectangleSize, Rectangle previous, Direction direction)
        {
            return direction switch
            {
                Direction.Top => new Point(0, previous.Height),
                Direction.Right => new Point(previous.Width, previous.Height - rectangleSize.Height),
                Direction.Bottom => new Point(previous.Width - rectangleSize.Width, -rectangleSize.Height),
                Direction.Left => new Point(-rectangleSize.Width, 0),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static Point CalculateRectangleLocation(Rectangle previous, Size rectangleSize, Direction direction)
        {
            var offset = GetOffset(rectangleSize, previous, direction);
            return new Point(previous.X + offset.X, previous.Y + offset.Y);
        }
    }
}