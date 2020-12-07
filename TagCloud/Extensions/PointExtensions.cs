using System;
using System.Drawing;

namespace TagCloud.Extensions
{
    public static class PointExtensions
    {
        public static Point CenterWith(this Point point, Size size)
        {
            return new Point(
                point.X + size.Width / 2,
                point.Y + size.Height / 2);
        }

        public static double DistanceBetween(this Point from, Point to)
        {
            var x = from.X - to.X;
            var y = from.Y - to.Y;
            return Math.Sqrt(x * x + y * y);
        }
    }
}
