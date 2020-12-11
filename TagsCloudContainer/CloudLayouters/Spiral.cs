using System;
using System.Drawing;

namespace TagsCloudContainer
{
    class Spiral
    {
        private double radius;
        private double angle;
        private readonly double radiusStep;
        private const double AngleStep = 0.1;
        public Point Center { get; set; }
        private const double RadiusStepCoefficient = 0.06;
        private const int ScaleDivider = 50;
        private readonly CircularCloudLayouter cloudDrawer;


        public Spiral(Point center, CircularCloudLayouter cloudDrawer)
        {
            this.cloudDrawer = cloudDrawer;
            Center = center;
            radius = 0;
            radiusStep = RadiusStepCoefficient * center.X / ScaleDivider;
        }

        public Point GetNextPosition(Size rectangleSize)
        {
            var position = new Point(
                (int)(Center.X + radius * Math.Cos(angle) - rectangleSize.Width / 2),
                (int)(Center.Y - radius * Math.Sin(angle)) - rectangleSize.Height / 2);
            radius += radiusStep;
            angle += AngleStep;
            return position;
        }

        public void ShiftRectangle(ref Rectangle rectangle, int step = 1)
        {
            var previousPosition = rectangle.Location;
            while (true)
            {
                ShiftAlongCoordinate(ref rectangle, previousPosition.X - Center.X + rectangle.Width / 2, step, 0);
                ShiftAlongCoordinate(ref rectangle, previousPosition.Y - Center.Y + rectangle.Height / 2, 0, step);
                if (previousPosition == rectangle.Location)
                    break;
                previousPosition = rectangle.Location;
            }
        }

        private void ShiftAlongCoordinate(ref Rectangle rectangle, int relativeCoordinate, int stepX, int stepY)
        {
            if (Math.Abs(relativeCoordinate) > 1)
            {
                if (relativeCoordinate >= 0)
                {
                    stepX = -stepX;
                    stepY = -stepY;
                }

                var checkRectangle = new Rectangle(new Point(rectangle.X + stepX, rectangle.Y + stepY), rectangle.Size);
                if (!cloudDrawer.CheckIntersections(checkRectangle))
                    rectangle.Offset(stepX, stepY);
            }
        }

        public void RollBackRadius(int pixelsValue)
        {
            radius = Math.Max(0, radius - pixelsValue);
        }
    }
}
