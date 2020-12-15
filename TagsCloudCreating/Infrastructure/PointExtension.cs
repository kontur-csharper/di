using System;
using System.Drawing;

namespace TagsCloudLayouters.Infrastructure
{
    internal static class PointExtension
    {
        public static double DistanceTo(this Point first, Point second) => Math.Sqrt(
            (first.X - second.X) * (first.X - second.X) + (first.Y - second.Y) * (first.Y - second.Y)
        );
    }
}