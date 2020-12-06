using System;
using System.Drawing;
using System.Linq;
using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App.Fractals
{
    public class DragonPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly DragonSettings settings;

        public DragonPainter(IImageHolder imageHolder, DragonSettings settings, Palette palette)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
            this.palette = palette;
        }

        public void Paint()
        {
            var imageSize = imageHolder.GetImageSize();
            var size = Math.Min(imageSize.Width, imageSize.Height) / 2.1f;
            using var brush = new SolidBrush(palette.PrimaryColor);
            using var backgroundBrush = new SolidBrush(palette.BackgroundColor);
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
                var r = new Random();
                var cosa = (float) Math.Cos(settings.Angle1);
                var sina = (float) Math.Sin(settings.Angle1);
                var cosb = (float) Math.Cos(settings.Angle2);
                var sinb = (float) Math.Sin(settings.Angle2);
                var shiftX = settings.ShiftX * size * 0.8f;
                var shiftY = settings.ShiftY * size * 0.8f;
                var scale = settings.Scale;
                var p = new PointF(0, 0);
                foreach (var i in Enumerable.Range(0, settings.IterationsCount))
                {
                    graphics.FillRectangle(brush, imageSize.Width / 3f + p.X, imageSize.Height / 2f + p.Y, 1,
                        1);
                    if (r.Next(0, 2) == 0)
                        p = new PointF(scale * (p.X * cosa - p.Y * sina), scale * (p.X * sina + p.Y * cosa));
                    else
                        p = new PointF(scale * (p.X * cosb - p.Y * sinb) + shiftX,
                            scale * (p.X * sinb + p.Y * cosb) + shiftY);
                    if (i % 100 == 0) imageHolder.UpdateUi();
                }
            }

            imageHolder.UpdateUi();
        }
    }
}