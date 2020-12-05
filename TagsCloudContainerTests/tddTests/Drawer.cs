using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class Drawer
    {
        public static Bitmap DrawRectangles(IEnumerable<Rectangle> rectangles)
        {
            var bitmap = new Bitmap(800, 800);
            using var graph = Graphics.FromImage(bitmap);

            var random = new Random();
            foreach (var rectangle in rectangles)
            {
                using var pen = new Pen(GetRandomColor(random), 2);
                graph.DrawRectangle(pen, rectangle);
            }

            return bitmap;
        }
        
        private static Color GetRandomColor(Random random)
        {
            return Color.FromArgb(
                random.Next(256),
                random.Next(256),
                random.Next(256));
        }
    }
}