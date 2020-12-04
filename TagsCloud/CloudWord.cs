using System.Drawing;

namespace TagsCloud
{
    public class CloudWord
    {
        public readonly string Value;
        public readonly Rectangle Rectangle;
        public readonly Font Font;
        public readonly Color Color;

        public CloudWord(string value, Rectangle rectangle, Font font, Color color)
        {
            Value = value;
            Rectangle = rectangle;
            Font = font;
            Color = color;
        }
    }
}