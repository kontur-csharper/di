using System.Drawing;

namespace TagsCloudContainer.Infrastructure.CloudGenerator
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}