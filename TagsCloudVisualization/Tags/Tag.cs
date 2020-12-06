using System.Drawing;

namespace TagsCloudVisualization.Tags
{
    public class Tag
    {
        public string Text { get; }
        public Rectangle BoundingZone { get; set; }
        public Font Font { get; }

        public Tag(string text, Rectangle boundingZone, Font font)
        {
            Text = text;
            BoundingZone = boundingZone;
            Font = font;
        }
    }
}