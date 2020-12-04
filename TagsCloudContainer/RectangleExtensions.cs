using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public static class RectangleExtensions
    {
        public static double GetDiagonal(this Rectangle rectangle)
        {
            return Math.Sqrt(rectangle.Height * rectangle.Height + rectangle.Width * rectangle.Width);
        }
    }
}