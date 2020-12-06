using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordCloudGenerator
{
    public class CircularLayouter : ILayouter
    {
        private readonly List<RectangleF> rectangles = new List<RectangleF>();
        private readonly Spiral spiral;

        public CircularLayouter(Point center)
        {
            spiral = new Spiral(center);
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException(
                    $"{(rectangleSize.Height <= 0 ? "Height" : "Width")} cant be negative or zero");

            var rectangleToAdd = new RectangleF {Size = rectangleSize, Location = spiral.GetNextPoint()};

            do
            {
                rectangleToAdd.Location = spiral.GetNextPoint();
            } while (rectangles.Any(rectangle => rectangle.IntersectsWith(rectangleToAdd)));

            if (rectangles.Count > 2 && !rectangles.Any(rectangleToAdd.IntersectsWith))
                rectangleToAdd = Fit(rectangleToAdd, rectangles, spiral.Center);

            rectangles.Add(rectangleToAdd);
            return rectangleToAdd;
        }

        public RectangleF[] GetRectangles()
        {
            return rectangles.ToArray();
        }

        private RectangleF Fit(RectangleF rectToFit, IEnumerable<RectangleF> others, Point center)
        {
            var iterationCount = 0;
            while (rectToFit.Location != center)
            {
                rectToFit = ShiftHorizontal(rectToFit, others, center);
                rectToFit = ShiftVertical(rectToFit, others, center);

                iterationCount++;

                if (others.Any(rect => rectToFit.IntersectsVerticallyWith(rect)) &&
                    others.Any(rect => rectToFit.IntersectsHorizontallyWith(rect)) || iterationCount > 10)
                    break;
            }

            return rectToFit;
        }

        private RectangleF ShiftVertical(RectangleF rectToShift, IEnumerable<RectangleF> others, Point center)
        {
            var dir = center.Y > rectToShift.Y ? 1 : -1;
            while (Math.Abs(rectToShift.Y - center.Y) > 0.5)
            {
                if (others.Any(rect => rectToShift.IntersectsVerticallyWith(rect)))
                    break;

                rectToShift.Offset(new PointF(0, dir));
            }

            return rectToShift;
        }

        private RectangleF ShiftHorizontal(RectangleF rectToShift, IEnumerable<RectangleF> others, Point center)
        {
            var dir = center.X > rectToShift.X ? 1 : -1;
            while (Math.Abs(rectToShift.X - center.X) > 0.5)
            {
                if (others.Any(rect => rectToShift.IntersectsHorizontallyWith(rect)))
                    break;

                rectToShift.Offset(new PointF(dir, 0));
            }

            return rectToShift;
        }
    }
}