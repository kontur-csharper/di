using System;
using System.Drawing;
using HomeExercise.settings;

namespace HomeExercise
{
    public class Spiral : ISpiral
    {
        public Point Center { get; }
        private readonly float step;
        private Point? previousPoint;
        private float angle;

        public Spiral(SpiralSettings settings)
        {
            step = settings.Step;
            angle = settings.Angle;
            previousPoint = null;
            Center = settings.Center;
        }

        public Point GetNextPoint()
        {
            while (true)
            {
                if (previousPoint == null)
                {
                    previousPoint = Center;
                    return Center;
                }
                
                angle++;
                var currentPoint = CalculateCurrentPoint();
                
                if (previousPoint.Equals(currentPoint))
                    continue;
                
                previousPoint = currentPoint;
                return currentPoint;
            }
        }

        private Point CalculateCurrentPoint()
        {
            var distance = angle * step;
            var currentX = distance * (float) Math.Cos(angle) + Center.X;
            var currentY = distance * (float) Math.Sin(angle) + Center.Y;
            
            return new Point((int) currentX, (int) currentY);
        }
    }
}