using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Layouter
{
    public class TargetVector
    {
        private readonly Point target;
        private Point location;

        public TargetVector(Point to, Point from)
        {
            target = to;
            location = from;
        }

        public IEnumerable<Point> GetPartialDelta()
        {
            while (target != location)
            {
                var delta = GetDelta();
                yield return new Point(delta.X, 0);
                yield return new Point(0, delta.Y);
                location.Offset(delta.X, delta.Y);
            }
        }

        private Point GetDelta()
        {
            var dx = target.X - location.X;
            var dy = target.Y - location.Y;
            return new Point(GetOffsetPerCoordinate(dx), GetOffsetPerCoordinate(dy));
        }

        public static int GetOffsetPerCoordinate(int coordinate)
        {
            if (coordinate > 0)
                return 1;
            if (coordinate < 0)
                return -1;
            return 0;
        }
    }
}
