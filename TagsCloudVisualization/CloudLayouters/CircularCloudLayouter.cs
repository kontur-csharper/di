using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.PointProviders;

namespace TagsCloudVisualization.CloudLayouters
{
    public class CircularCloudLayouter : ICloudLayout
    {
        private readonly IPointProvider pointProvider;

        public CircularCloudLayouter(IPointProvider pointProvider)
        {
            this.pointProvider = pointProvider;
            Rectangles = new List<Rectangle>();
        }

        public List<Rectangle> Rectangles { get; }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height < 0 || rectangleSize.Width < 0)
                throw new ArgumentException("Width or height of size was negative");

            var rectangle = GetRectangle(rectangleSize);
            Rectangles.Add(rectangle);

            return rectangle;
        }


        private Rectangle GetRectangle(Size rectangleSize)
        {
            Rectangle rectangle;
            do
            {
                rectangle = new Rectangle(pointProvider.GetPoint(), rectangleSize);
            } while (IsCollide(rectangle));

            return rectangle;
        }

        private bool IsCollide(Rectangle rectangle)
        {
            return Rectangles.Any(rectangle.IntersectsWith)
                   || rectangle.X < 0 || rectangle.Y < 0;
        }
    }
}