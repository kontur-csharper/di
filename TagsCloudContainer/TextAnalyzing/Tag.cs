using System.Drawing;

namespace TagsCloudContainer.TextAnalyzing
{
    public class Tag
    {
        public readonly Font Font;
        public readonly Rectangle Rectangle;
        public readonly string Text;

        public Tag(Rectangle rectangle, string text, Font font)
        {
            Rectangle = rectangle;
            Text = text;
            Font = font;
        }
    }
}