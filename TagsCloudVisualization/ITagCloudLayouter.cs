using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}