using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public class WordRectangle
    {
        public Rectangle Rectangle { get; }
        public string Text { get; }
        public int FontSize { get; }
        public string FontFamily { get; }
        public string FontColor { get; }

        public WordRectangle(Rectangle rectangle, string text, int fontSize, string fontFamily, string fontColor)
        {
            Rectangle = rectangle;
            Text = text;
            FontSize = fontSize;
            FontFamily = fontFamily;
            FontColor = fontColor;
        }
    }
}