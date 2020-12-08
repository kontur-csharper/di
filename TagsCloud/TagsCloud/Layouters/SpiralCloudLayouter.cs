using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.Layouters
{
    public class SpiralCloudLayouter : ICloudLayouter
    {
        private Point center;
        private int step;
        private readonly List<Rectangle> rectangles;
        private (int Width, int Height) imageHolderSize;

        public SpiralCloudLayouter(ImageSettings settings, int step = 1)
        {
            rectangles = new List<Rectangle>();
            center = new Point(settings.Width / 2, settings.Height / 2);
            imageHolderSize = (settings.Width, settings.Height);
            this.step = step;
        }

        public void ClearLayouter()
        {
            rectangles.Clear();
            step = 1;
        }

        public void UpdateCenterPoint(ImageSettings settings)
        {
            center = new Point(settings.Width / 2, settings.Height / 2);
            imageHolderSize = (settings.Width, settings.Height);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var spiral = new Spiral(center, step);
            while (true)
            {
                var point = spiral.GetNextPoint();
                var newRectangle = GetRectangleByCenter(rectangleSize, point);

                if (!RectangleIntersectWithOthers(newRectangle))
                {
                    rectangles.Add(newRectangle);
                    return newRectangle;
                }
            }
        }

        private Rectangle GetRectangleByCenter(Size rectangleSize, Point rectangleCenter)
        {
            var leftTopAngle = new Point(rectangleCenter.X - rectangleSize.Width / 2,
                rectangleCenter.Y - rectangleSize.Height / 2);
            return new Rectangle(leftTopAngle, rectangleSize);
        }

        private bool RectangleIntersectWithOthers(Rectangle checkedRectangle)
        {
            foreach (var rectangle in rectangles)
            {
                if (rectangle.IntersectsWith(checkedRectangle))
                {
                    return true;
                }
            }

            return false;
        }
        private bool InBoundsOfImage(Rectangle rectangle)
        {
            return rectangle.Left >= 0
                   && rectangle.Right <= imageHolderSize.Width
                   && rectangle.Top >= 0
                   && rectangle.Bottom <= imageHolderSize.Height;
        }
    }
}