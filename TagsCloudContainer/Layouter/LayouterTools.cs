using System;
using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public static class LayouterTools
    {
        public static double CalculateDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }
    }
}