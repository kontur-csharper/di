using System.Drawing;

namespace TagsCloud.App
{
    public static class RectangleExtensions
    {
        public static bool IsNestedInImage(this Rectangle rectangle, ImageSize imageSize) =>
            rectangle.Left >= 0
            && rectangle.Right < imageSize.Width
            && rectangle.Top >= 0
            && rectangle.Bottom < imageSize.Height;
    }
}