using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class RectangleVisualizer : IVisualizer
    {
        public Bitmap GetBitmap(List<Rectangle> rectangles)
        {
            var imageSize = GetImageSize(rectangles);
            var pen = new Pen(Color.MediumVioletRed, 4);

            var bitmap = new Bitmap(imageSize.Width + (int) pen.Width,
                imageSize.Height + (int) pen.Width);
            using var graphics = Graphics.FromImage(bitmap);

            if (rectangles.Count != 0) graphics.DrawRectangles(pen, rectangles.ToArray());

            return bitmap;
        }

        protected Size GetImageSize(IEnumerable<Rectangle> rectangles)
        {
            var width = 0;
            var height = 0;

            foreach (var rectangle in rectangles)
            {
                if (rectangle.Left < 0 || rectangle.Top < 0)
                    throw new ArgumentException("rectangle out of image boundaries");

                if (width < rectangle.Right) width = rectangle.Right;
                if (height < rectangle.Bottom) height = rectangle.Bottom;
            }

            return new Size(width, height);
        }
    }
}