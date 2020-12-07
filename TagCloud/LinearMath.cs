using System;
using System.Drawing;

namespace TagCloud
{
    public static class LinearMath
    {
        public static double GetDiagonal(Size size)
        {
            return Math.Sqrt(size.Width * size.Width + size.Height * size.Height);
        }

        public static Point PolarToCartesian(Point center, double radius, double angle)
        {
            var x = center.X + (int)(radius * (float)Math.Cos(angle));
            var y = center.Y + (int)(radius * (float)Math.Sin(angle));
            return new Point(x, y);
        }

        public static double LinearInterpolate(double leftBound, double rightBound, double value)
        {
            return Math.Min(Math.Max((value - leftBound) / (rightBound - leftBound), 0), 1);
        }
    }
}
