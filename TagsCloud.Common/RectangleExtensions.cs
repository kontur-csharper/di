using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.Common
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle rect, IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Any(rect.IntersectsWith);
        }
    }
}