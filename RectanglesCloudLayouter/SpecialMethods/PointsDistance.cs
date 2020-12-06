using System;
using System.Drawing;

namespace RectanglesCloudLayouter.SpecialMethods
{
    public static class PointsDistance
    {
        public static int GetCeilingDistanceBetweenPoints(Point first, Point second) =>
            (int) Math.Ceiling(Math.Sqrt((first.X - second.X) * (first.X - second.X) +
                                         (first.Y - second.Y) * (first.Y - second.Y)));
    }
}