using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public interface ILayoutAlgorithm : IEnumerable<Point>
    {
        void AddUsedPoints(IEnumerable<Point> points);
    }
}