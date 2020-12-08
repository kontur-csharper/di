using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.Layouters
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private readonly List<Point> potentialPosingPoints;
        private (int Width, int Height) imageHolderSize;
        private Point Center { get; set; }
        private bool isFirst = true;

        public CircularCloudLayouter(ImageSettings settings)
        {
            imageHolderSize = (settings.Width, settings.Height);
            Center = new Point(settings.Width / 2, settings.Height / 2);
            potentialPosingPoints = new List<Point> { Center };
        }

        public void ClearLayouter()
        {
            rectangles.Clear();
            potentialPosingPoints.Clear();
            isFirst = true;
        }

        public Point GetCenterPoint() => Center;

        public void UpdateCenterPoint(ImageSettings settings)
        {
            imageHolderSize = (settings.Width, settings.Height);
            Center = new Point(settings.Width / 2, settings.Height / 2);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (isFirst)
                return PutFirstRectangle(rectangleSize);

            var rect = PlaceRectangle(new Rectangle(Center, rectangleSize));
            rectangles.Add(rect);
            potentialPosingPoints.AddRange(GetPotentialPosingPointsFromRectangle(rect));

            RemovePoints();
            return rect;
        }

        public void RemovePoints()
        {
            var pointsToRemove = potentialPosingPoints.Where(point =>
                GetLocationVariations(point, new Rectangle(0, 0, 1, 1))
                    .Select(x => new Rectangle(x, new Size(1, 1)))
                    .All(rect => rectangles.Any(x => x.IntersectsWith(rect))))
                .ToHashSet();

            potentialPosingPoints.RemoveAll(x => pointsToRemove.Contains(x));
        }

        private Rectangle PutFirstRectangle(Size rectangleSize)
        {
            var rect = new Rectangle(
                new Point(Center.X - rectangleSize.Width / 2, (Center.Y - rectangleSize.Height / 2)),
                rectangleSize);
            rectangles.Add(rect);
            potentialPosingPoints.AddRange(GetPotentialPosingPointsFromRectangle(rect));
            isFirst = false;

            return rect;
        }

        private Rectangle PlaceRectangle(Rectangle rect)
        {
            var orderedPoints = potentialPosingPoints.OrderBy(GetDistanceToCenter);
            var isPlaced = false;

            foreach (var point in orderedPoints)
            {
                foreach (var location in GetLocationVariations(point, rect))
                {
                    rect.Location = location;
                    if (rectangles.Any(x => x.IntersectsWith(rect)) || !InBoundsOfImage(rect)) 
                        continue;
                    isPlaced = true;
                    break;
                }

                if (isPlaced)
                    break;
            }

            if (!isPlaced)
                throw new BadImageFormatException(
                    "Недостаточно места для размещения слова, увеличьте размер изображения, или уменьшите размер шрифта");

            return rect;
        }

        private double GetDistanceToCenter(Point firstPoint)
        {
            var dx = Math.Max(firstPoint.X, Center.X) - Math.Min(firstPoint.X, Center.X);
            var dy = Math.Max(firstPoint.Y, Center.Y) - Math.Min(firstPoint.Y, Center.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private List<Point> GetLocationVariations(Point potentialPoint, Rectangle rect)
        {
            return new List<Point>
            {
                new Point(potentialPoint.X, potentialPoint.Y),
                new Point(potentialPoint.X - rect.Width , potentialPoint.Y),
                new Point(potentialPoint.X - rect.Width, potentialPoint.Y - rect.Height),
                new Point(potentialPoint.X, potentialPoint.Y - rect.Height)
            };
        }

        private List<Point> GetPotentialPosingPointsFromRectangle(Rectangle rect)
        {
            var points = new List<Point>();
            if (isFirst)
                points.AddRange(GetPotentialPosingPointsForFirstRect(rect));

            points.Add(new Point(rect.Location.X + rect.Width, rect.Location.Y));
            points.Add(new Point(rect.Location.X + rect.Width, rect.Location.Y + rect.Height));
            points.Add(new Point(rect.Location.X, rect.Location.Y + rect.Height));
            points.Add(rect.Location);

            return points;
        }

        private List<Point> GetPotentialPosingPointsForFirstRect(Rectangle rect)
        {
            return new List<Point>
            {
                new Point(rect.Location.X + rect.Width / 2, rect.Location.Y),
                new Point(rect.Location.X, rect.Location.Y + rect.Height / 2),
                new Point(rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height),
                new Point(rect.Location.X + rect.Width, rect.Location.Y + rect.Height / 2)
            };
        }

        private bool InBoundsOfImage(Rectangle rectangle)
        {
            return rectangle.Left >= 0 
                   && rectangle.Right <= imageHolderSize.Width 
                   && rectangle.Top >= 0 
                   && rectangle.Bottom <= imageHolderSize.Height;
        }
    }
}