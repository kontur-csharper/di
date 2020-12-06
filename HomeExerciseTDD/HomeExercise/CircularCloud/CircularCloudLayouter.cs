using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HomeExercise
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        public Point Center { get; }
        private readonly List<Rectangle> rectanglesInCloud = new List<Rectangle>();
        private readonly ISpiral spiral;

        public CircularCloudLayouter(ISpiral spiral)
        {
            Center = spiral.Center;
            this.spiral = spiral;
        }
        
        private bool IsIntersect(Rectangle currentRectangle)
        {
            return rectanglesInCloud.Any(currentRectangle.IntersectsWith);
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (!IsSizeValidity(rectangleSize))
                throw new ArgumentException();
            while (true)
            {
                var rectangleLocation = spiral.GetNextPoint();
                var currentRectangle = new Rectangle(rectangleLocation, rectangleSize);
                if (IsIntersect(currentRectangle)) continue;
                rectanglesInCloud.Add(currentRectangle);
                return currentRectangle;
            }
        }

        private bool IsSizeValidity(Size size)
        {
            return size.Height >= 0 && size.Width >= 0;
        }
    }
}