using System;
using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public class Tag
    {
        public readonly Font Font;
        public readonly string Value;
        public Rectangle Rectangle;

        public Tag(string value, Font font, Rectangle rectangle)
        {
            Value = value;
            Font = font;
            Rectangle = rectangle;
        }

        public Tag RescaleTag(float tagsCloudSizeRatio)
        {
            var tagBounds = new Rectangle((int) (Rectangle.X * tagsCloudSizeRatio),
                (int) (Rectangle.Y * tagsCloudSizeRatio),
                (int) (Rectangle.Width * tagsCloudSizeRatio),
                (int) (Rectangle.Height * tagsCloudSizeRatio));
            var tagFontSize = Math.Max((int) (Font.Size * tagsCloudSizeRatio), 1);
            return new Tag(Value, new Font(Font.FontFamily, tagFontSize, Font.Style), tagBounds);
        }
    }
}