using System;
using System.Drawing;

namespace TagCloud
{
    public class Spiral
    {
        private double angle;
        private Point center;
        private double rayLength;

        public Spiral(Point center)
        {
            this.center = center;
            rayLength = 0;
            angle = -1 * Math.PI / 180;
        }

        public Point GetNextPoint()
        {
            angle += Math.PI / 180;
            rayLength = 1d / (2 * Math.PI) * angle;
            var xCoordinate = (int) (center.X + rayLength * Math.Cos(angle));
            var yCoordinate = (int) (center.Y + rayLength * Math.Sin(angle));
            return new Point(xCoordinate, yCoordinate);
        }
    }
}