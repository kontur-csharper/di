using System.Drawing;
using System.Linq;
using TagCloud.TagClouds;

namespace TagCloud.Visualizers
{
    public class RectangleVisualizer : IVisualizer<RectangleTagCloud>
    {
        private readonly SolidBrush brush = new SolidBrush(Color.SlateGray);

        public RectangleVisualizer(RectangleTagCloud cloud)
        {
            VisualizeTarget = cloud;
        }

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
