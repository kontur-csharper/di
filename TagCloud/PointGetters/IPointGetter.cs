using System.Drawing;

namespace TagCloud.PointGetters
{
    public interface IPointGetter
    {
        public Point Center { get; }
        public Point GetNextPoint();
    }
}
