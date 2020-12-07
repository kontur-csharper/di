using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TagCloud.Extensions;
using TagCloud.Settings;

namespace TagCloud.Layouters
{
    public class CircularCloudLayouter : ILayouter
    {
        private readonly Point center;
        private readonly CircularPositioner positioner;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public CircularCloudLayouter(CloudSettings settings) : this(settings.Center, settings.StartRadius)
        {
        }

        public CircularCloudLayouter(Point center,
            double startRadius,
            double searchAngleStep = Math.PI / 2,
            double iterationOffsetAngle = Math.PI / 180)
        {
            this.center = center;
            positioner = new CircularPositioner(
                center,
                startRadius,
                searchAngleStep,
                iterationOffsetAngle);
        }

        public Rectangle PutNextRectangle(Size size)
        {
            if (size == null)
                throw new ArgumentException("Size should not be null");
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException($"Some side was negative in size: {size.Width}x{size.Height}");

            var stepRadius = LinearMath.GetDiagonal(size) / 2;
            var bestPoint = new Point(int.MaxValue, int.MaxValue);
            var bestDistance = double.PositiveInfinity;
            foreach (var point in positioner.GetIteration(point => CanBePlaced(new Rectangle(point, size)), stepRadius))
            {
                var pointDistance = center.DistanceBetween(point.CenterWith(size));
                if (pointDistance > bestDistance)
                    continue;

                bestPoint = point;
                bestDistance = pointDistance;
            }

            return CreateRectangle(bestPoint, size);
        }

        private Rectangle CreateRectangle(Point point, Size size)
        {
            var rectangle = new Rectangle(point, size);
            rectangles.Add(rectangle);
            return rectangle;
        }

        private bool CanBePlaced(Rectangle targetRectangle)
        {
            return rectangles.All(rectangle => !targetRectangle.IntersectsWith(rectangle));
        }
    }
}
