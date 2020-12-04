using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.PointsLayouts
{
    public class SpiralPoints : IPointsLayout
    {
        private readonly HashSet<Point> generatedPoints = new HashSet<Point>{new Point()};
        private int radius = 1;
        private int pointsInCircleCount = 4;
        
        public IEnumerable<Point> GetPoints()
        {
            foreach (var point in generatedPoints)
                yield return point;
            
            while (true)
            {
                var circlePoints = GetCirclePoints();
                foreach (var point in circlePoints)
                    generatedPoints.Add(point);
                radius += 8;
                pointsInCircleCount = radius;

                foreach (var point in circlePoints)
                    yield return point;
            }
        }

        private HashSet<Point> GetCirclePoints()
        {
            var circlePoints = new HashSet<Point>();
            
            for (var i = 0; i < pointsInCircleCount; ++i)
            {
                var x = (int)(Math.Cos(2 * Math.PI * i / pointsInCircleCount) * radius + 0.5);
                var y = (int)(Math.Sin(2 * Math.PI * i / pointsInCircleCount) * radius + 0.5);

                var point = new Point(x, y);
                circlePoints.Add(point);
            }

            return circlePoints;
        }
    }
}