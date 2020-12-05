using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Surface
    {
        private readonly List<Rectangle> firstQuarterRectangles = new List<Rectangle>();
        private readonly List<Rectangle> secondQuarterRectangles = new List<Rectangle>();
        private readonly List<Rectangle> thirdQuarterRectangles = new List<Rectangle>();
        private readonly List<Rectangle> fourthQuarterRectangles = new List<Rectangle>();
        private readonly Point center;
        
        public Surface(Point center)
        {
            this.center = center;
        }
        
        public void AddRectangle(Rectangle rect)
        {
            var rectQuarters = FindQuartersForRectangle(rect);

            foreach (var quarter in rectQuarters)
            {
                GetRectanglesFromQuarter(quarter).Add(rect);
            }
        }

        public bool RectangleIntersectsWithOther(Rectangle rect)
        {
            var rectQuarters = FindQuartersForRectangle(rect);

            return rectQuarters.Any(quarter =>
                GetRectanglesFromQuarter(quarter).Any(
                    rectFromQuarter => rectFromQuarter.IntersectsWith(rect)));
        }

        public Rectangle GetShiftedToCenterRectangle(Rectangle rect)
        {
            if (CalculateRectangleCenter(rect) == center)
                return rect;

            while (true)
            {
                var movedRect = DoStepToCenter(rect);
                if (RectangleIntersectsWithOther(movedRect))
                    return rect;
                rect = movedRect;
            }
        }

        private static Point CalculateRectangleCenter(Rectangle rect)
        {
            return rect.Location + rect.Size / 2;
        }

        public IEnumerable<Quarters> FindQuartersForRectangle(Rectangle rect)
        {
            var rectangleQuarters = new HashSet<Quarters>();
            var decoratedRectangle = new RectangleDecorator(rect);

            foreach (var corner in decoratedRectangle.GetCorners())
            {
                var quarter = GetQuarterForPoint(corner);
                if (quarter != Quarters.Unknown)
                {
                    rectangleQuarters.Add(quarter);
                }
            }

            return rectangleQuarters;
        }

        private List<Rectangle> GetRectanglesFromQuarter(Quarters quarter)
        {
            return quarter switch
            {
                Quarters.First => firstQuarterRectangles,
                Quarters.Second => secondQuarterRectangles,
                Quarters.Third => thirdQuarterRectangles,
                Quarters.Fourth => fourthQuarterRectangles,
                _ => throw new ArgumentException()
            };
        }

        private Rectangle DoStepToCenter(Rectangle rect)
        {
            var deltaPoints = FindQuartersForRectangle(rect)
                .Select(GetDeltaForQuarter)
                .ToArray();

            rect.Offset(GetResultDeltaPoint(deltaPoints));

            return rect;
        }

        private static Point GetResultDeltaPoint(Point[] deltaPoints)
        {
            if (deltaPoints.Length < 2)
                return deltaPoints[0];

            var resultDelta = deltaPoints[0];
            resultDelta.Offset(deltaPoints[1]);
            
            if (Math.Abs(resultDelta.X) == 2)
                resultDelta.X = 1 * Math.Sign(resultDelta.X);
            if (Math.Abs(resultDelta.Y) == 2)
                resultDelta.Y = 1 * Math.Sign(resultDelta.Y);

            return resultDelta;
        }

        private static Point GetDeltaForQuarter(Quarters quarter)
        {
            return quarter switch
            {
                Quarters.First => new Point(-1, 1),
                Quarters.Second => new Point(1, 1),
                Quarters.Third => new Point(1, -1),
                Quarters.Fourth => new Point(-1, -1),
                _ => throw new ArgumentException()
            };
        }

        private Quarters GetQuarterForPoint(Point point)
        {
            return (point.X, point.Y) switch
            {
                var (x, y) when x > center.X && y < center.Y => Quarters.First,
                var (x, y) when x < center.X && y < center.Y => Quarters.Second,
                var (x, y) when x < center.X && y > center.Y => Quarters.Third,
                var (x, y) when x > center.X && y > center.Y => Quarters.Fourth,
                _ => Quarters.Unknown
            };
        }

        public enum Quarters
        {
            First,
            Second,
            Third,
            Fourth,
            Unknown
        }
    }
}