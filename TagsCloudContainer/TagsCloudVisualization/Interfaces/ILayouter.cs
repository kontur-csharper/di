using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Interfaces
{
    public interface ILayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}