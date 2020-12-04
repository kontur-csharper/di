using System.Drawing;

namespace TagsCloud.Layouter
{
    public interface IRectanglesLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
