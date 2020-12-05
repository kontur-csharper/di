using System.Drawing;

namespace TagsCloudContainer
{
    public class FontColorCreator : IFontColorCreator
    {
        private readonly Color fontColor;

        public FontColorCreator(Color fontColor)
        {
            this.fontColor = fontColor;
        }

        public Color GetFontColor(int wordsCount, int maxWordsCount)
        {
            return fontColor;
        }
    }
}