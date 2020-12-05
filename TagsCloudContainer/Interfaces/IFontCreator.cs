namespace TagsCloudContainer
{
    public interface IFontCreator
    {
        string GetFontName(int wordsCount, int maxWordsCount);
        float GetFontSize(int wordsCount, int maxWordsCount);
    }
}