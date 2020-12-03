using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Extensions
{
    public static class PointExtension
    {
        internal static Point GetMaxDistanceToLayoutBorder(
            this Point point, IEnumerable<Rectangle> rectangles)
        {
            var rects = rectangles.ToList();
            var deltaXToRight = rects
                .Max(rectangle => rectangle.Right - point.X);
            var deltaXToLeft = rects
                .Max(rectangle => point.X - rectangle.Left);

            var deltaYToBottom = rects
                .Max(rectangle => rectangle.Bottom - point.Y);
            var deltaYToUp = rects
                .Max(rectangle => point.Y - rectangle.Top);

            var maxDeltaX = Math.Max(deltaXToLeft, deltaXToRight);
            var maxDeltaY = Math.Max(deltaYToBottom, deltaYToUp);

            return new Point(maxDeltaX, maxDeltaY);
        }
    }
}