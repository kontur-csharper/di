﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class Drawer
    {
        public static Bitmap DrawImage(List<ICloudTag> rectangles, IWordConfig config)
        {
            CheckParameters(rectangles, config.Center);

            var image = new Bitmap(config.Center.X + GetDeltaX(rectangles), 
                config.Center.Y + GetDeltaY(rectangles));//TODO 
            using var graphics = Graphics.FromImage(image);

            foreach (var rectangle in rectangles)
            {
                graphics.DrawString(rectangle.Text, config.Font, new SolidBrush(config.TextColor), rectangle.Size);
                graphics.DrawRectangle(new Pen(config.TextColor),rectangle.Size);//TODO remove
                Console.WriteLine(rectangle.Text);
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
            return rectangles.Select(elem => elem.Size.Right).Max();
        }

        private static int GetDeltaY(List<ICloudTag> rectangles)
        {
            return rectangles.Select(elem => elem.Size.Bottom).Max();
        }
    }
}