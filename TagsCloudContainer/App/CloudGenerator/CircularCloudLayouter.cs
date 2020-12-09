using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.App.Utils;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.CloudGenerator
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private const int Shift = 1;
        public readonly Point Center;
        private readonly List<Rectangle> currentRectangles;
        private double currentAngle;
        private int currentRadius;

        public CircularCloudLayouter(Point center)
        {
            Center = center;
            currentRectangles = new List<Rectangle>();
            currentRadius = 0;
            currentAngle = 0;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (currentRectangles.Count == 0)
                return PutFirstRectangle(rectangleSize);

            Rectangle? newRectangle = null;
            while (true)
            {
                var point = GetNextPoint();
                var movedRectangles = GetRectanglesAroundPoint(point, rectangleSize)
                    .Where(rect => !rect.IntersectsWith(currentRectangles))
                    .Select(rect => MoveToCenter(rect));

                foreach (var rectangle in movedRectangles)
                    if (newRectangle == null
                        || rectangle.GetDistanceToPoint(Center) < newRectangle.Value.GetDistanceToPoint(Center))
                        newRectangle = rectangle;

                if (newRectangle != null)
                {
                    currentRectangles.Add(newRectangle.Value);
                    return newRectangle.Value;
                }
            }
        }

        private Rectangle PutFirstRectangle(Size rectangleSize)
        {
            var firstRectangle = new Rectangle(
                new Point(
                    Center.X - rectangleSize.Width / 2,
                    Center.Y - rectangleSize.Height / 2),
                rectangleSize);
            currentRectangles.Add(firstRectangle);
            return firstRectangle;
        }

        private Point GetNextPoint()
        {
            if (currentRadius == 0)
            {
                currentRadius++;
                return Center;
            }

            var point = new Point(
                (int) (Center.X + currentRadius * Math.Cos(currentAngle)),
                (int) (Center.Y + currentRadius * Math.Sin(currentAngle)));
            currentAngle += Math.PI / (6 + currentRadius);
            if (Math.Abs(currentAngle - 2 * Math.PI) < 0.0000001)
            {
                currentAngle = 0;
                currentRadius++;
            }

            return point;
        }

        private static Rectangle[] GetRectanglesAroundPoint(Point point, Size size)
        {
            return new[]
            {
                new Rectangle(point, size),
                new Rectangle(point.X - size.Width, point.Y, size.Width, size.Height),
                new Rectangle(point.X, point.Y - size.Height, size.Width, size.Height),
                new Rectangle(point.X - size.Width, point.Y - size.Height, size.Width, size.Height)
            };
        }

        private Rectangle MoveToCenter(Rectangle rectangle)
        {
            var resultRectangle = new Rectangle(rectangle.Location, rectangle.Size);
            Rectangle? result;
            while ((result = GetMovedRectangle(resultRectangle)) != null) resultRectangle = result.Value;
            return resultRectangle;
        }

        private Rectangle? GetMovedRectangle(Rectangle rectangle)
        {
            foreach (var direction in DirectionUtils.Directions)
            {
                var tempRectangle = rectangle.GetMovedCopy(direction, Shift);
                if (!tempRectangle.IntersectsWith(currentRectangles)
                    && !(tempRectangle.GetDistanceToPoint(Center) >= rectangle.GetDistanceToPoint(Center)))
                    return tempRectangle;
            }

            return null;
        }
    }
}