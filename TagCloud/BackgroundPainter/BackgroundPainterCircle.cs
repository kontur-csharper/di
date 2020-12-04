using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.BackgroundPainter
{
    public class BackgroundPainterCircle: IBackgroundPainter
    {
        public void Draw(List<Tuple<string, Rectangle>> tags, ICanvas canvas, Graphics graphics)
        {
            var radius = Math.Min(canvas.Width, canvas.Height) / 2;
            var rectangle = new Rectangle(canvas.Center.X - radius, canvas.Center.Y - radius,
                radius * 2, radius * 2);
            
            graphics.FillEllipse(new SolidBrush(Color.Gray), rectangle);
        }
    }
}