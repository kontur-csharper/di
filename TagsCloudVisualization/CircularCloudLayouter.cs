using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ITagCloudLayouter
    {
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private readonly IPoints Points;

        public CircularCloudLayouter(IPoints points) =>
            Points = points;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var halfRectangleSize = new Size(rectangleSize.Width / 2, rectangleSize.Height / 2);
            var currentPoints = Points.GetPoints()
                                      .First(point => rectangles
                                                 .All(y => !y.IntersectsWith(new Rectangle(point - halfRectangleSize, rectangleSize))));
            var rectangle = new Rectangle(currentPoints - halfRectangleSize, rectangleSize);
            rectangles.Add(rectangle);
            return rectangle;
        }
    }
}