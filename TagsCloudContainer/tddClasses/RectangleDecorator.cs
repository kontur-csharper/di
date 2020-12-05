using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class RectangleDecorator
    {
        public readonly Rectangle DecoratedRectangle;

        public RectangleDecorator(Rectangle rectangleToDecorate)
        {
            DecoratedRectangle = rectangleToDecorate;
        }

        public IEnumerable<Point> GetCorners()
        {
            yield return new Point(DecoratedRectangle.Left, DecoratedRectangle.Top);
            yield return new Point(DecoratedRectangle.Right, DecoratedRectangle.Top);
            yield return new Point(DecoratedRectangle.Right, DecoratedRectangle.Bottom);
            yield return new Point(DecoratedRectangle.Left, DecoratedRectangle.Bottom);
        }
    }
}