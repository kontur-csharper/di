using System;
using System.Drawing;

namespace WordCloudGenerator
{
    public class Spiral
    {
        public Point Center { get; }
        private double angle;

        public Spiral(Point center)
        {
            Center = center;
        }

        public Point GetNextPoint()
        {
            var x = (int) (angle * Math.Cos(angle) + Center.X);
            var y = (int) (angle * Math.Sin(angle) + Center.Y);
            angle += Math.PI / 180;
            return new Point(x, y);
        }
    }
}