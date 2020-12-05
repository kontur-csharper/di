using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Interfaces
{
    public interface ISpiral
    {
        public Point Center { get; }
        public SpiralType Type { get; }
        public Point GetNextPoint();
    }
}