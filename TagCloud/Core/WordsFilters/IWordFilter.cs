namespace TagCloud.Core.WordsFilters
{
    public interface IWordFilter
    {
        bool IsValid(string word);
    }
}