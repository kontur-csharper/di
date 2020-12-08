using System;
using System.Drawing;

namespace TagsCloud.Layouters
{
    internal class Spiral
    {
        private Point center;
        private readonly int step;
        private double currentAngle;
        public Spiral(Point center, int step)
        {
            this.center = center;
            this.step = step;
            currentAngle = 0;
        }

        public Point GetNextPoint()
        {
            var x = (int)Math.Round(step * currentAngle * Math.Cos(currentAngle)) + center.X;
            var y = (int)Math.Round(step * currentAngle * Math.Sin(currentAngle)) + center.Y;
            currentAngle += 0.05;
            return new Point(x, y);
        }
    }
}