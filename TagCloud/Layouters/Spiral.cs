using System;
using System.Drawing;

namespace TagCloud.Layouters
{
    public class Spiral
    {
        private Point center;
        private int spiralPitch;
        private double angleStepRadian;
        private double nextAngle;
        
        public Spiral(Point center, int spiralPitch, double angleStepRadian)
        {
            this.center = center;
            this.spiralPitch = spiralPitch;
            this.angleStepRadian = angleStepRadian;
        }

        public Point GetNextPoint()
        {
            var distance = GetPolarRadius();
            var point = ConvertFromPolarToPoint(distance, nextAngle);
            nextAngle += angleStepRadian;
            return point;
        }

        public Quadrant Quadrant => (Quadrant) ((nextAngle - angleStepRadian - 0.25 * Math.PI) 
            % (2 * Math.PI) / (0.5 * Math.PI));

        private double GetPolarRadius() => nextAngle * spiralPitch / (2 * Math.PI);

        private Point ConvertFromPolarToPoint(double distance, double angleRadian)
        {
            var pointX = center.X + Convert.ToInt32(Math.Round(distance * Math.Cos(angleRadian)));
            var pointY = center.Y + Convert.ToInt32(Math.Round(distance * Math.Sin(angleRadian)));
            return new Point(pointX, pointY);
        }
    }
}