using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle rect, IEnumerable<Rectangle> otherRects)
        {
            return otherRects.Any(rect.IntersectsWith);
        }
    }
}