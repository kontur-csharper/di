using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly Spiral spiral;
        private readonly Surface surface;

        public Point Center { get; }

        public CircularCloudLayouter(Point center, Spiral spiral)
        {
            Center = center;
            this.spiral = spiral;
            surface = new Surface(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rect = new Rectangle(GetNextPointForRect(rectangleSize), rectangleSize);
            while (surface.RectangleIntersectsWithOther(rect))
            {
                rect.Location = GetNextPointForRect(rectangleSize);
            }

            rect = surface.GetShiftedToCenterRectangle(rect);

            surface.AddRectangle(rect);
            spiral.Reset();
            return rect;
        }

        private Point GetNextPointForRect(Size rectangleSize)
        {
            var rectLocation = GetRectLocationFromCenter(spiral.GetNextPoint(), rectangleSize);
            rectLocation.Offset(Center);
            return rectLocation;
        }

        private static Point GetRectLocationFromCenter(Point rectCenter, Size rectSize)
        {
            return rectCenter - rectSize / 2;
        }
    }
}