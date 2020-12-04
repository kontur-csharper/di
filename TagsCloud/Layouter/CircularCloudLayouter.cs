using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter.Base;

namespace TagsCloud.Layouter
{
    public class CircularCloudLayouter : RectanglesLayouterBase
    {
        private readonly Point center;
        private int index;

        public CircularCloudLayouter(Point center) : base(center)
        {
            this.center = center;
        }

        protected override IEnumerable<Point> GetPoints()
        {
            while (true)
            {
                var x = (int)(index * Math.Cos(index * 0.5)) + center.X;
                var y = (int)(index * Math.Sin(index * -0.5)) + center.Y;
                index++;
                yield return new Point(x, y);
            }
        }
    }
}
