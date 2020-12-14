using System;
using System.Drawing;

namespace TagsCloudVisualizationTests
{
    public static class Utils
    {
        public static double GetDistance(Point start, Point end) =>
            Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
    }
}