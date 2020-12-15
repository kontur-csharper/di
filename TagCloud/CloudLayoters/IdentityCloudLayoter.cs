using System.Drawing;
using TagCloud.PointGetters;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.CloudLayoters
{
    public class IdentityCloudLayoter : ICloudLayoter
    {
        public IPointGetter PointGetter { get; set; }

        public int Top { get; private set; }

        public int Bottom { get; private set; }

        public int Left { get; private set; }

        public int Right { get; private set; }

        private readonly HashSet<Rectangle> rectangles = new HashSet<Rectangle>();

        public bool IsEmpty => !rectangles.Any();

        public IdentityCloudLayoter(IPointGetter getter = null)
        {
            PointGetter = getter;
            if (getter != null)
            {
                Top = getter.Center.Y;
                Bottom = getter.Center.Y;
                Right = getter.Center.X;
                Left = getter.Center.X;
            }
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = this.PutRectangleWithoutIntersection(rectangles, rectangleSize);
            ChangeSize(rectangle);
            rectangles.Add(rectangle);
            return rectangle;
        }

        private void ChangeSize(Rectangle rectangle)
        {
            if (IsEmpty)
            {
                Top = rectangle.Top;
                Bottom = rectangle.Bottom;
                Right = rectangle.Right;
                Left = rectangle.Left;
            }
            if (rectangle.Top < Top)
                Top = rectangle.Top;
            if (rectangle.Bottom > Bottom)
                Bottom = rectangle.Bottom;
            if (rectangle.Left < Left)
                Left = rectangle.Left;
            if (rectangle.Right > Right)
                Right = rectangle.Right;
        }
    }
}
