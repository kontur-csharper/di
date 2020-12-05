using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class UlamSpiral : ISpiral
    {
        private readonly IEnumerator<Point> points;
        private Point currentPoint;

        public UlamSpiral(Point center)
        {
            Center = center;
            ValidateParameters();
            currentPoint = center;
            points = GetPoints().GetEnumerator();
            Type = SpiralType.UlamSpiral;
        }

        public Point Center { get; }
        public SpiralType Type { get; }

        public Point GetNextPoint()
        {
            points.MoveNext();
            return points.Current;
        }

        private IEnumerable<Point> GetPoints()
        {
            var count = 0;
            yield return currentPoint;

            while (true)
            {
                for (var i = 0; i < count; i++)
                {
                    currentPoint.X += count % 2 == 0 ? 1 : -1;
                    yield return currentPoint;
                }

                for (var i = 0; i < count; i++)
                {
                    currentPoint.Y += count % 2 == 0 ? 1 : -1;
                    yield return currentPoint;
                }

                count++;
            }
        }

        private void ValidateParameters()
        {
            if (Center.X < 0 || Center.Y < 0)
                throw new ArgumentException("Center coordinates should not be negative numbers");
        }
    }
}