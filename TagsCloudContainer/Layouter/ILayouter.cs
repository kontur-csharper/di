using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public interface ILayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);

        public void SetCenter(Point center);
    }
}