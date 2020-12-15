using System;
using System.Drawing;

namespace TagCloud.PointGetters
{
    internal class SpiralPointGetter : IPointGetter
    {
        public Point Center { get; }

        public double Gomotation { get; private set; }

        private double radius;

        public SpiralPointGetter(Point? center = null, double gomotation = 1.0)
        {
            Center = center != null ? center.Value : Point.Empty;
            Gomotation = gomotation;
        }

        internal void ChangeGomotation(double g) => Gomotation = g;

        public Point GetNextPoint()
        {
            var x = (int)(radius * Math.Cos(radius));
            var y = (int)(radius * Math.Sin(radius));
            ChangePolarCoordinate();
            return new Point(x + Center.X, y + Center.Y);
        }

        private void ChangePolarCoordinate() => radius += Gomotation;
    }
}
