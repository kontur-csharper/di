using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.CloudTags;
using TagsCloudVisualization.Configs;

namespace TagsCloudVisualization
{
    public static class Drawer
    {
        public static Bitmap DrawImage(List<ICloudTag> rectangles, IConfig config)
        {
            CheckParameters(rectangles, config.Center);

            var actualSize = new Size(config.Center.X + GetDeltaX(rectangles),
                config.Center.Y + GetDeltaY(rectangles));
            var size = new Size(Math.Max(actualSize.Width, config.ImageSize.Width),
                Math.Max(actualSize.Height, config.ImageSize.Height));
            var image = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(image);

            foreach (var rectangle in rectangles)
            {
                graphics.DrawString(rectangle.Text, config.Font, new SolidBrush(config.TextColor), rectangle.Rectangle);
                graphics.DrawRectangle(new Pen(config.TextColor), rectangle.Rectangle);
            }

            return image;
        }

        private static void CheckParameters(List<ICloudTag> rectangles, Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("X or Y of center was negative");

            if (!rectangles.Any())
                throw new ArgumentException("The sequence contains no elements");
        }

        private static int GetDeltaX(List<ICloudTag> rectangles)
        {
            return rectangles.Select(elem => elem.Rectangle.Right).Max();
        }

        private static int GetDeltaY(List<ICloudTag> rectangles)
        {
            return rectangles.Select(elem => elem.Rectangle.Bottom).Max();
        }
    }
}