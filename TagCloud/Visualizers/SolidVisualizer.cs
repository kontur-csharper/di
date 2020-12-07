using System.Drawing;
using System.Linq;
using TagCloud.TagClouds;

namespace TagCloud.Visualizers
{
    public class SolidVisualizer : IVisualizer<RectangleTagCloud>
    {
        private readonly SolidBrush brush;

        public SolidVisualizer(RectangleTagCloud cloud, Color color)
        {
            VisualizeTarget = cloud;
            Color = color;
            brush = new SolidBrush(color);
        }

        public Color Color { get; }

        public RectangleTagCloud VisualizeTarget { get; }

        public void Draw(Graphics graphics)
        {
            var leftUpBound = VisualizeTarget.LeftUpBound;
            graphics.TranslateTransform(-leftUpBound.X, -leftUpBound.Y);
            if (VisualizeTarget.Count > 0)
                graphics.FillRectangles(brush, VisualizeTarget.ToArray());
        }
    }
}
