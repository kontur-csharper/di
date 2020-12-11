using System.Drawing;

namespace TagsCloud.App
{
    public class Word
    {
        public readonly Font Font;
        public readonly Rectangle Rectangle;
        public readonly string Text;

        public Word(string text, Font font, Rectangle rectangle)
        {
            Text = text;
            Font = font;
            Rectangle = rectangle;
        }
    }
}