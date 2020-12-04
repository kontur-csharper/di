using System;
using System.Drawing;

namespace TagCloud.Layout
{
    internal class Spiral : ISpiral
    {
        private readonly Point center;
        private const int Step = 1;
        private const double AngleStep = 0.05;
        private double currentAngle;
        public Spiral(ICanvas canvas)
        {
            center = canvas.Center;
            currentAngle = 0;
        }

        public Point GetNextPoint()
        {
            var x = (int) Math.Round(Step * currentAngle * Math.Cos(currentAngle)) + center.X;
            var y = (int) Math.Round(Step * currentAngle * Math.Sin(currentAngle)) + center.Y;
            currentAngle += AngleStep;
            return new Point(x, y);
        }
    }
}