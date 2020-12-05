using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class CloudLayouter : ILayouter
    {
        public CloudLayouter(ISpiral spiral)
        {
            Spiral = spiral;
            Center = spiral.Center;
            Rectangles = new List<Rectangle>();
        }

        private Point Center { get; }
        private ISpiral Spiral { get; }
        public List<Rectangle> Rectangles { get; }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("rectangle Height or Size should not be negative or zero");

            return GetNewRectangle(rectangleSize);
        }

        private Rectangle GetNewRectangle(Size rectangleSize)
        {
            Rectangle rectangle;

            do
            {
                var location = Spiral.GetNextPoint();
                rectangle = new Rectangle(location, rectangleSize);
            } while (Collided(rectangle));

            rectangle = MoveCloserToCenter(rectangle);
            Rectangles.Add(rectangle);

            return rectangle;
        }

        private Rectangle MoveCloserToCenter(Rectangle rectangle)
        {
            var movedRectangle = rectangle;

            while (!Collided(rectangle) &&
                   rectangle.X != Center.X &&
                   rectangle.Y != Center.Y)
            {
                movedRectangle = rectangle;
                var deltaX = Center.X - rectangle.X < 0 ? -1 : 1;
                var deltaY = Center.Y - rectangle.Y < 0 ? -1 : 1;

                var position = new Point(rectangle.X + deltaX, rectangle.Y + deltaY);
                rectangle = new Rectangle(position, rectangle.Size);
            }

            return movedRectangle;
        }

        private bool Collided(Rectangle newRectangle)
        {
            return Rectangles.Any(rectangle => rectangle.IntersectsWith(newRectangle));
        }
    }
}