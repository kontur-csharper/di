using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    internal class CircularCloudLayouter : ICloudLayouter
    {
        private const int RollBackPixelsCount = 10; // Оптимальное значение величины отката радиуса спирали в пикселях
        private bool buildingStarted;
        private Spiral spiral;
        public IReadOnlyCollection<Rectangle> Rectangles { get; set; }

        public CircularCloudLayouter()
        {
            Reset();
        }

        public void Reset()
        {
            Rectangles = new List<Rectangle>();
            spiral = new Spiral(new Point(100, 100), this);
            buildingStarted = false;
        }

        public void ChangeCenter(Point newCenter)
        {
            if (buildingStarted)
            {
                var offset = new Point(newCenter.X - spiral.Center.X, newCenter.Y - spiral.Center.Y);
                foreach (var rectangle in Rectangles)
                    rectangle.Offset(offset);
            }
            spiral = new Spiral(newCenter, this);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            buildingStarted = true;
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException(
                    $"Invalid rectangleSize = ({rectangleSize.Width}, {rectangleSize.Height})", nameof(rectangleSize));
            spiral.RollBackRadius(RollBackPixelsCount);
            while (true)
            {
                var currentCoordinate = spiral.GetNextPosition(rectangleSize);
                var currentRectangle = new Rectangle(currentCoordinate, rectangleSize);

                if (!CheckIntersections(currentRectangle))
                {
                    spiral.ShiftRectangle(ref currentRectangle);
                    ((List<Rectangle>)Rectangles).Add(currentRectangle);
                    return currentRectangle;
                }
            }
        }

        public bool CheckIntersections(Rectangle rectangle)
        {
            return Rectangles.Any(otherRectangle => rectangle.IntersectsWith(otherRectangle));
        }
    }
}