using System.Drawing;

namespace WordCloudGenerator
{
    public static class RectangleExtensions
    {
        public static RectangleF Shift(this RectangleF rectangle, PointF shift)
        {
            rectangle.Offset(shift);
            return rectangle;
        }

        public static bool IntersectsHorizontallyWith(this RectangleF first, RectangleF second)
        {
            return first.IntersectsWith(second) && (first.Left <= second.Left && first.Right >= second.Left ||
                                                    first.Left <= second.Right && first.Right >= second.Right);
        }

        public static bool IntersectsVerticallyWith(this RectangleF first, RectangleF second)
        {
            return first.IntersectsWith(second) && (first.Top <= second.Top && first.Bottom >= second.Top ||
                                                    first.Top <= second.Bottom && first.Bottom >= second.Bottom);
        }
    }
}