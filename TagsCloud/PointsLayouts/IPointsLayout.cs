using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.PointsLayouts
{
    public interface IPointsLayout
    {
        IEnumerable<Point> GetPoints();
    }
}