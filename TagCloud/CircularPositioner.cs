using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class CircularPositioner
    {
        private const double DoublePi = Math.PI * 2;
        private const double MinStep = 1d;
        private const double RandomRadius = 2;
        private const double OneHalf = 0.5;

        private readonly Point center;
        private readonly double iterationOffset;
        private readonly double radiusThreshold;
        private readonly Random random = new Random();
        private readonly double searchAngleStep;
        private double startAngle;

        public CircularPositioner(
            Point center,
            double radiusThreshold,
            double searchAngleStep,
            double iterationOffset)
        {
            this.center = center;
            this.radiusThreshold = radiusThreshold;
            this.searchAngleStep = searchAngleStep;
            this.iterationOffset = iterationOffset;
        }

        public IEnumerable<Point> GetIteration(Func<Point, bool> isValid, double radiusStep)
        {
            var angle = startAngle;
            while (angle < startAngle + DoublePi)
            {
                var point = GetClosestPlaceToCenter(angle, isValid, radiusStep);
                yield return point;

                angle += searchAngleStep;
            }

            startAngle += iterationOffset + GetRandomScalar(RandomRadius);
        }

        private Point GetClosestPlaceToCenter(double angle, Func<Point, bool> isValid, double radiusStep)
        {
            var direction = -1;
            var radius = radiusThreshold;
            var point = LinearMath.PolarToCartesian(center, radius, angle);
            var step = radiusStep;

            while (!isValid(point))
                point = LinearMath.PolarToCartesian(center, radius += step, angle);
            var bestPoint = point;
            while (step > MinStep)
            {
                point = LinearMath.PolarToCartesian(center, radius += step * direction, angle);
                if (isValid(point))
                {
                    bestPoint = point;
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }

                step /= 2;
            }

            return bestPoint;
        }

        private double GetRandomScalar(double radius)
        {
            return (random.NextDouble() - OneHalf) * radius;
        }
    }
}
