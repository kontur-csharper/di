using System;
using System.Drawing;

namespace TagCloud.Core.LayoutAlgorithms
{
    public class ArchimedeanSpiral : ISpiral
    {
        public Point Start { get; }
        private double angle;
        private readonly double deltaAngle;
        private readonly double spiralOffset;

        public ArchimedeanSpiral(Point start,
            int step = 5, double deltaAngle = Math.PI / 360)
        {
            Start = start;
            this.deltaAngle = deltaAngle;
            angle = 0d;
            spiralOffset = step / (Math.PI * 2);
        }

        public PointF GetNextPoint()
        {
            var nextPoint = new PointF(
                (float) (angle * Math.Cos(angle) * spiralOffset + Start.X),
                (float) (angle * Math.Sin(angle) * spiralOffset + Start.Y)
            );
            angle += deltaAngle;

            return nextPoint;
        }
    }
}