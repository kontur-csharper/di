using System;
using System.Drawing;

namespace TagCloud.PointGetters
{
    public class CirclePointGetter : IPointGetter
    {
        public Point Center { get; }
        private double angle;
        private int radius;
        private double dangle => Math.PI / (2 * radius);
        public CirclePointGetter(Point? center = null) => Center = center != null ? center.Value : Point.Empty;
        public Point GetNextPoint()
        {
            var x = (int)(radius * Math.Cos(angle));
            var y = (int)(radius * Math.Sin(angle));
            ChangePolarCoordinate();
            return new Point(x + Center.X, y + Center.Y);
        }
        private void ChangePolarCoordinate()
        {
            if (radius == 0 || angle > 2 * Math.PI)
            {
                radius++;
                angle = 0;
            }
            else
                angle += dangle;
        }
    }
}
