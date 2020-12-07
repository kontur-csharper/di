using System.Drawing;

namespace TagsCloud.Common
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}