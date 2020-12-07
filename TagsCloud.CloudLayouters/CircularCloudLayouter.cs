using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Common;

namespace TagsCloud.CloudLayouters
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        private readonly ISpiral spiral;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public CircularCloudLayouter(ISpiral spiral)
        {
            this.spiral = spiral;
        }

        public Rectangle PutNextRectangle(Size rectSize)
        {
            if (rectSize.Width <= 0 || rectSize.Height <= 0)
                throw new ArgumentException("rectangle's width and height must be positive numbers");
            var rect = GetNextRectangle(rectSize);
            rectangles.Add(rect);
            return rect;
        }

        private Rectangle GetNextRectangle(Size rectSize)
        {
            while (true)
            {
                var rectLocation = spiral.GetNextPoint() - new Size(rectSize.Width / 2, rectSize.Height / 2);
                var rect = new Rectangle(rectLocation, rectSize);
                if (!rect.IntersectsWith(rectangles))
                    return rect;
            }
        }
    }
}
