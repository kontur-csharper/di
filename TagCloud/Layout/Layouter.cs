using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Layout
{
    public class Layouter : ILayouter
    {
        private readonly List<Rectangle> rectangles;
        private readonly ISpiral spiral;
        private readonly ICanvas canvas;

        public Layouter(ISpiral spiral, ICanvas canvas)
        {
            rectangles = new List<Rectangle>();
            this.spiral = spiral;
            this.canvas = canvas;
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var point = spiral.GetNextPoint();
            while (PointBelongsCanvas(point))
            {
                var newRectangle = GetRectangleByCenter(rectangleSize, point);
                
                if (!RectangleIntersectWithOthers(newRectangle))
                {
                    rectangles.Add(newRectangle);
                    return newRectangle;
                }

                point = spiral.GetNextPoint();
            }
            return Rectangle.Empty;
        }

        private bool PointBelongsCanvas(Point point)
        {
            return point.X > 0 && point.X < canvas.Width && point.Y > 0 && point.Y < canvas.Height;
        }

        private Rectangle GetRectangleByCenter(Size rectangleSize, Point rectangleCenter)
        {
            var leftTopAngle = new Point(rectangleCenter.X - rectangleSize.Width / 2,
                rectangleCenter.Y - rectangleSize.Height / 2);
            return new Rectangle(leftTopAngle, rectangleSize);
        }
        
        private bool RectangleIntersectWithOthers(Rectangle checkedRectangle)
        {
            return rectangles.Count(rect => rect.IntersectsWith(checkedRectangle)) > 0;
        }
    }
}