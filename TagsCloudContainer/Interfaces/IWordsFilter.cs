namespace TagsCloudContainer.Interfaces
{
    public interface IWordsFilter
    {
        string[] GetInterestingWords(string[] words);
    }
}