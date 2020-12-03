namespace TagCloud.Core.WordsFilters
{
    public class BoringWordsFilter : IWordFilter
    {
        public bool IsValid(string word)
        {
            return word.Length >= 4;
        }
    }
}