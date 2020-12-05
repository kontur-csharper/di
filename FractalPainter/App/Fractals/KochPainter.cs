using System;
using System.Drawing;
using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App.Fractals
{
    public class KochPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private Size imageSize;

        public KochPainter(IImageHolder imageHolder, Palette palette)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
        }

        public void Paint()
        {
            imageSize = imageHolder.GetImageSize();
            using (var graphics = imageHolder.StartDrawing())
            using (var backgroundBrush = new SolidBrush(palette.BackgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
                DrawSegment(graphics, 0, imageSize.Height*0.9f, imageSize.Width, imageSize.Height*0.9f, true);
            }
            imageHolder.UpdateUi();
        }

        private void DrawSegment(Graphics graphics, float x0, float y0, float x1, float y1, bool primaryColor)
        {
            var len2 = (x0 - x1)*(x0 - x1) + (y0 - y1)*(y0 - y1);
            if (len2 < 4)
            {
                if (y0 < 0 || y1 < 0) return;
                using (var penBrush = new SolidBrush(primaryColor ? palette.PrimaryColor : palette.SecondaryColor))
                {
                    var pen = new Pen(penBrush, 3);
                    graphics.DrawLine(pen, x0, y0, x1, y1);
                }
            }
            else
            {
                var vx = (x1 - x0)/3;
                var vy = (y1 - y0)/3;
                DrawSegment(graphics, x0, y0, x0 + vx, y0 + vy, primaryColor);
                var k = (float) Math.Sqrt(3)/2f;
                var px = (x0 + x1)/2 + vy*k;
                var py = (y0 + y1)/2 - vx*k;
                DrawSegment(graphics, x0 + vx, y0 + vy, px, py, !primaryColor);
                DrawSegment(graphics, px, py, x0 + 2*vx, y0 + 2*vy, !primaryColor);
                DrawSegment(graphics, x0 + 2*vx, y0 + 2*vy, x1, y1, primaryColor);
            }
        }
    }
}