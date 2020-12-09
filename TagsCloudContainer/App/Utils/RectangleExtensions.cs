using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.App.Utils
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle currentRectangle, IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Any(rect => currentRectangle.IntersectsWith(rect));
        }

        public static double GetDistanceToPoint(this Rectangle rectangle, Point point)
        {
            var rectCenter = new Point(
                rectangle.X + rectangle.Width / 2,
                rectangle.Y + rectangle.Height / 2);
            return Math.Sqrt(Math.Pow(point.X - rectCenter.X, 2) + Math.Pow(point.Y - rectCenter.Y, 2));
        }

        public static Rectangle GetMovedCopy(this Rectangle rectangle, DirectionToMove direction, int shift)
        {
            var location = new Point(rectangle.X, rectangle.Y);
            switch (direction)
            {
                case DirectionToMove.Up:
                    location.Y -= shift;
                    break;
                case DirectionToMove.Down:
                    location.Y += shift;
                    break;
                case DirectionToMove.Left:
                    location.X -= shift;
                    break;
                case DirectionToMove.Right:
                    location.X += shift;
                    break;
            }

            return new Rectangle(location, rectangle.Size);
        }

        public static int GetArea(this Rectangle rectangle)
        {
            return rectangle.Height * rectangle.Width;
        }
    }
}