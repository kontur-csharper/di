using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Spiral
    {
        private double currentAngle;
        private readonly double angleStep;
        private readonly double shiftFactor;

        private double Radius => shiftFactor * currentAngle;

        public Spiral(double angleStep = Math.PI / 18, double shiftFactor = 18 / Math.PI)
        {
            this.angleStep = angleStep;
            this.shiftFactor = shiftFactor;
        }

        public Point GetNextPoint()
        {
            var dx = Math.Cos(currentAngle) * Radius;
            var dy = Math.Sin(currentAngle) * Radius;
            currentAngle += angleStep;

            return new Point(
                (int) Math.Round(dx),
                (int) Math.Round(dy));
        }

        public void Reset()
        {
            currentAngle = 0;
        }
    }
}