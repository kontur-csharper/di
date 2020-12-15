using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.PointGetters;

namespace TagCloud.CloudLayoters
{
    public class DensityCloudLayouter : ICloudLayoter
    {
        internal readonly HashSet<Rectangle> rectangles = new HashSet<Rectangle>();
        public IPointGetter PointGetter { get; set; }
        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Right { get; private set; }
        public int Left { get; private set; }

        public bool IsEmpty => !rectangles.Any();

        public DensityCloudLayouter(IPointGetter getter = null)
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
            var result = MoveToCentre(this.PutRectangleWithoutIntersection(rectangles, rectangleSize));
            ChangeSize(result);
            rectangles.Add(result);
            return result;
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

        private Rectangle MoveToCentre(Rectangle rectangle)
        {
            var previousRectangle = new Rectangle();
            while (true)
            {
                var vectorToMove = GetVectorToMove(rectangle);
                rectangle = MoveToPixel(rectangle, vectorToMove);
                if (rectangle == previousRectangle)
                    break;
                previousRectangle = rectangle;
            }
            return rectangle;
        }

        private Rectangle MoveToPixel(Rectangle rectangle, Point vectorToMove)
        {
            if (Math.Abs(vectorToMove.X) > Math.Abs(vectorToMove.Y))
            {
                var newRectangleX = new Rectangle(rectangle.Location, rectangle.Size);
                newRectangleX.X += vectorToMove.X > 0 ? 1 : -1;
                if (!rectangles.HasIntersection(newRectangleX))
                    return newRectangleX;
            }
            var newRectangle = new Rectangle(rectangle.Location, rectangle.Size);
            newRectangle.Y += vectorToMove.Y > 0 ? 1 : vectorToMove.Y < 0 ? -1 : 0;
            if (!rectangles.HasIntersection(newRectangle))
                return newRectangle;
            return rectangle;
        }

        private Point GetVectorToMove(Rectangle rectangle)
        {
            var centreRectangle = GetCentreRectangle(rectangle);
            return new Point(this.Center().X - centreRectangle.X, this.Center().Y - centreRectangle.Y);
        }

        private Point GetCentreRectangle(Rectangle rectangle)
        {
            var yCentreRectangle = (int)((rectangle.Top + rectangle.Bottom) / 2);
            var xCentreRectangle = (int)((rectangle.Left + rectangle.Right) / 2);
            var centreRectangle = new Point(xCentreRectangle, yCentreRectangle);
            return centreRectangle;
        }
    }
}
