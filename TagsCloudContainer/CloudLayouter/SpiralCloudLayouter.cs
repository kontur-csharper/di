using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.CloudLayouter
{
    public class SpiralCloudLayouter : ICloudLayouter
    {
        private readonly ImageSettings imageSettings;
        private Spiral spiral;

        public SpiralCloudLayouter(ImageSettings imageSettings)
        {
            this.imageSettings = imageSettings;
            var center = new Point(imageSettings.Width / 2, imageSettings.Height / 2);
            spiral = new Spiral(center);
            Rectangles = new List<Rectangle>();
        }

        public List<Rectangle> Rectangles { get; private set; }

        public void Dispose()
        {
            var center = new Point(imageSettings.Width / 2, imageSettings.Height / 2);
            spiral = new Spiral(center);
            Rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var center = new Point(imageSettings.Width / 2, imageSettings.Height / 2);
            if (spiral.Center != center)
                spiral = new Spiral(center);
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException();

            var rectangle = Rectangles.Count == 0
                ? new Rectangle(GetCoordinatesOfRectangle(rectangleSize), rectangleSize)
                : Rectangles.Last();
            while (RectangleIntersectAnyRectangles(rectangle))
            {
                var coordinates = GetCoordinatesOfRectangle(rectangleSize);
                rectangle = new Rectangle(new Point(coordinates.X, coordinates.Y), rectangleSize);
            }

            Rectangles.Add(rectangle);
            return rectangle;
        }

        private Point GetCoordinatesOfRectangle(Size rectangleSize)
        {
            var pointOnSpiral = spiral.GetNextPointOnSpiral();
            var x = pointOnSpiral.X;
            var y = pointOnSpiral.Y;
            if (y < spiral.Center.Y)
                y -= rectangleSize.Height;
            if (x < spiral.Center.X)
                x -= rectangleSize.Width;
            return new Point(x, y);
        }

        private bool RectangleIntersectAnyRectangles(Rectangle rectangle)
        {
            foreach (var anotherRectangle in Rectangles)
                if (rectangle.IntersectsWith(anotherRectangle))
                    return true;

            return false;
        }
    }
}