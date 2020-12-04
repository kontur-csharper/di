using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.PointsLayouts
{
    public class SquarePoints : IPointsLayout
    {
        private readonly HashSet<Point> generatedPoints = new HashSet<Point>{new Point()};
        private int radius = 1;
        
        public IEnumerable<Point> GetPoints()
        {
            foreach (var point in generatedPoints)
                yield return point;
            
            while (true)
            {
                var squarePoints = GetSquarePoints();
                foreach (var point in squarePoints)
                    generatedPoints.Add(point);
                radius+=10;

                foreach (var point in squarePoints)
                    yield return point;
            }
        }

        private HashSet<Point> GetSquarePoints()
        {
            var squarePoints = new HashSet<Point>();

            for (var x = -radius; x <= radius; x++)
            {
                if (Math.Abs(x) != radius)
                {
                    squarePoints.Add(new Point(x, radius));
                    squarePoints.Add(new Point(x, -radius));
                    continue;
                }
                for (var y = -radius; y <= radius; y++)
                {
                    squarePoints.Add(new Point(x, y));
                }
            }

            return squarePoints;
        }
    }
}