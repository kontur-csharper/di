using System.Drawing;

namespace TagsCloudContainer.TagsCloudContainer
{
    public class Tag
    {
        public Tag(string text, Rectangle rectangle, Font font, Brush textColor)
        {
            Text = text;
            Rectangle = rectangle;
            Font = font;
            TextColor = textColor;
        }

        public string Text { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }
        public Brush TextColor { get; }

        public Tag ChangeFontFamily(string fontFamily)
        {
            return new Tag(Text, Rectangle, new Font(fontFamily, Font.Size), TextColor);
        }

        public Tag ChangeTextColor(Brush color)
        {
            return new Tag(Text, Rectangle, Font, color);
        }
    }
}