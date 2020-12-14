using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SpiralPoints : IPoints
    {
        private Point center;

        public SpiralPoints(Point center) =>
            this.center = center;

        public IEnumerable<Point> GetPoints()
        {
            var radius = 0;
            while (true)
            {
                for (var i = 0; i < 360; i++)
                {
                    yield return new Point((int) (Math.Cos(2 * Math.PI * i / 360) * radius + 0.5) + center.X,
                                           (int) (Math.Sin(2 * Math.PI * i / 360) * radius + 0.5) + center.Y);
                }

                radius++;
            }
        }
    }
}