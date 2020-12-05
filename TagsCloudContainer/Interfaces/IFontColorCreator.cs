using System.Drawing;

namespace TagsCloudContainer
{
    public interface IFontColorCreator
    {
        Color GetFontColor(int wordsCount, int maxWordsCount);
    }
}