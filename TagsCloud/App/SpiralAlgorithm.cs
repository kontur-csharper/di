using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class SpiralAlgorithm : ILayoutAlgorithm
    {
        private readonly Point center;
        private readonly HashSet<Point> usedPoints;

        public SpiralAlgorithm(Point center)
        {
            this.center = center;
            usedPoints = new HashSet<Point>();
        }

        public IEnumerator<Point> GetEnumerator()
        {
            var currentPoint = center;
            if (!usedPoints.Contains(currentPoint))
                yield return currentPoint;
            var lineLength = 1;
            var direction = -1;

            while (true)
            {
                for (var i = 0; i < lineLength; i++)
                {
                    currentPoint = Point.Add(currentPoint, new Size(direction, 0));
                    if (!usedPoints.Contains(currentPoint))
                        yield return currentPoint;
                }

                for (var i = 0; i < lineLength; i++)
                {
                    currentPoint = Point.Add(currentPoint, new Size(0, direction));
                    if (!usedPoints.Contains(currentPoint))
                        yield return currentPoint;
                }

                direction *= -1;
                lineLength++;
            }
        }

        public void AddUsedPoints(IEnumerable<Point> points) => usedPoints.UnionWith(points);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}