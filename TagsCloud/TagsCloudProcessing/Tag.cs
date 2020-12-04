using System.Drawing;

namespace TagsCloud.TagsCloudProcessing
{
    public class Tag
    {
        public string Value { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }

        public Tag(string value, Rectangle rectangle, Font font)
        {
            Value = value;
            Rectangle = rectangle;
            Font = font;
        }
    }
}
