using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using HomeExercise.settings;


namespace HomeExercise
{
    public class CircularCloudRectanglesPainter: IPainter
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private Pen pen;
        private Color color;
        private readonly Random randomazer;
        private readonly List<Rectangle> rectangles;
        private readonly string fileName;
        private readonly ImageFormat format;
        private readonly int offsetX;
        private readonly int offsetY;

        public CircularCloudRectanglesPainter(List<Rectangle> rectangles,PainterSettings settings)
        {
            offsetX = settings.Width/2;
            offsetY = settings.Height/2;
            format = settings.Format;
            fileName = settings.FileName;
            this.rectangles = rectangles;
            randomazer= new Random();
            color = new Color();//??????
            bitmap = new Bitmap(settings.Width, settings.Height);//???????
            graphics = Graphics.FromImage(bitmap);//???????
        }

        public void DrawFigures()
        {
            var center = rectangles.First();
            foreach (var rectangle in rectangles)
            {
                var newX= rectangle.X + offsetX - center.X;
                var newY = rectangle.Y + offsetY - center.Y;
                var point = new Point(newX, newY);
                var newRectangle = new Rectangle(point, rectangle.Size);

                color = GetColor();
                pen = new Pen(color);
                graphics.DrawRectangle(pen, newRectangle);
            }
            
            bitmap.Save(fileName, format);
        }

        private Color GetColor()
        {
            var R = randomazer.Next(0, 255);
            var G = randomazer.Next(0, 255);
            var B = randomazer.Next(0, 255);
            
            return Color.FromArgb(R, G, B);
        }
    }
}