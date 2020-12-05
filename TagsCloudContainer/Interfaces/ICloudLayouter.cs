using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudLayouter
    {
        Point Center { get; }

        Rectangle PutNextRectangle(Size rectSize);
    }
}