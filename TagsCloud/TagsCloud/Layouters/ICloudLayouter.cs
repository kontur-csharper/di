using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.Layouters
{
    public interface ICloudLayouter
    {
        void UpdateCenterPoint(ImageSettings settings);
        void ClearLayouter();
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}