using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using TagCloud.PointGetters;

namespace TagCloud.CloudLayoters
{
    public static class CloudLayoterExtention
    {
        public static Point Center(this ICloudLayoter layoter) => layoter.PointGetter.Center;

        public static Size Size(this ICloudLayoter layoter) => 
            new Size(layoter.Right - layoter.Left, layoter.Bottom - layoter.Top);

        public static bool HasIntersection(this HashSet<Rectangle> rectangles, Rectangle rectangle) => 
            rectangles.Any(r => r.IntersectsWith(rectangle));

        public static Rectangle GetRectangleFromSizeAndCenter(Size size, Point rectangleCenter)
        {
            var location = new Point(
                rectangleCenter.X - (int)(size.Width / 2),
                rectangleCenter.Y - (int)(size.Height / 2));
            return new Rectangle(location, size);
        }

        public static Rectangle PutRectangleWithoutIntersection(this ICloudLayoter layoter, HashSet<Rectangle> rectangles, Size size)
        {
            Rectangle rectangle;
            do
            {
                var point = layoter.PointGetter.GetNextPoint();
                rectangle = GetRectangleFromSizeAndCenter(size, point);
            } while (rectangles.HasIntersection(rectangle));
            return rectangle;
        }

        public static void SetPointGetterIfNull(this ICloudLayoter layoter, IPointGetter getter) => layoter.PointGetter ??= getter;
    }
}
