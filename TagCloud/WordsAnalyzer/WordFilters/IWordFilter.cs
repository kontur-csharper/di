namespace TagCloud.WordsAnalyzer.WordFilters
{
    public interface IWordFilter
    {
        public bool ShouldExclude(string word);
    }
}