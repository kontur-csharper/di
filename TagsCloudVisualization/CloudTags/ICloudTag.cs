using System.Drawing;

namespace TagsCloudVisualization.CloudTags
{
    public interface ICloudTag
    {
        public Rectangle Rectangle { get; }
        public string Text { get; }
    }
}