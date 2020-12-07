using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.Extensions
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle rect, IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Any(i => i.IntersectsWith(rect));
        }

        public static Point GetMiddlePoint(this Rectangle rect)
        {
            var x = rect.X + rect.Width / 2;
            var y = rect.Y + rect.Height / 2;
            return new Point(x, y);
        }

        public static Rectangle CreateRectangleFromMiddlePointAndSize(Point middlePoint, Size rectangleSize)
        {
            var x = middlePoint.X - rectangleSize.Width / 2;
            var y = middlePoint.Y - rectangleSize.Height / 2;
            return new Rectangle(new Point(x, y), rectangleSize);
        }

        public static Rectangle MoveOneStepTowardsPoint(this Rectangle rect, Point to, int axisStep)
        {
            var middlePoint = rect.GetMiddlePoint();

            var dx = Math.Sign(to.X - middlePoint.X) * axisStep;
            var dy = Math.Sign(to.Y - middlePoint.Y) * axisStep;

            var movedMiddlePoint = new Point(middlePoint.X + dx, middlePoint.Y + dy);

            return CreateRectangleFromMiddlePointAndSize(movedMiddlePoint, rect.Size);
        }
    }
}
