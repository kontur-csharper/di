using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Extensions
{
    public static class RectangleExtensions
    {
        public static Point Center(this Rectangle rectangle)
        {
            return new Point(
                rectangle.X + rectangle.Width / 2,
                rectangle.Y + rectangle.Height / 2);
        }

        public static double MaxDistanceFromCorner(this Rectangle rectangle, Point anchor)
        {
            return rectangle.GetCorners().Select(point => anchor.DistanceBetween(point)).Max();
        }

        public static IEnumerable<Point> GetCorners(this Rectangle rectangle)
        {
            yield return new Point(rectangle.X, rectangle.Y);
            yield return new Point(rectangle.X + rectangle.Width, rectangle.Y);
            yield return new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            yield return new Point(rectangle.X, rectangle.Y + rectangle.Height);
        }
    }
}
