using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace RectanglesCloudLayouter.RectanglesCloudVisualization
{
    public static class CircularCloudVisualisation
    {
        private static Random _random = new Random();

        public static void MakeImageCloud(List<Rectangle> rectangles, int cloudRadius,
            string pathToSave, ImageFormat imageFormat)
        {
            if (rectangles.Count == 0)
                throw new Exception("IsNotContainsRectanglesForDraw");
            var imageSize = new Size(cloudRadius * 2, cloudRadius * 2);
            using var bitmap =
                new Bitmap(imageSize.Width, imageSize.Height);
            DrawCloud(bitmap, rectangles, imageSize);
            bitmap.Save(pathToSave, imageFormat);
        }

        private static void DrawCloud(Bitmap bitmap, List<Rectangle> rectangles, Size imageSize)
        {
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            foreach (var rectangle in rectangles)
            {
                var brush = GetNewRandomBrush();
                var currentLocation = rectangle.Location
                                      + imageSize / 2;
                graphics.FillRectangle(brush, new Rectangle(currentLocation, rectangle.Size));
            }

            graphics.Flush();
        }

        private static Brush GetNewRandomBrush()
        {
            return new SolidBrush(Color.FromArgb(_random.Next(255),
                _random.Next(255), _random.Next(255)));
        }
    }
}