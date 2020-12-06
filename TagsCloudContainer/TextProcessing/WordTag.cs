using System.Drawing;

namespace TagsCloudContainer.TextProcessing
{
    public class WordTag
    {
        public readonly string Text;
        public readonly Rectangle Rectangle;
        public readonly Font WordFont;

        public WordTag(string text, Rectangle rectangle, Font wordFont)
        {
            Text = text;
            Rectangle = rectangle;
            WordFont = wordFont;
        }
    }
}