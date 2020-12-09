namespace TagsCloudContainer.Infrastructure.TextAnalyzer
{
    public interface IWordFilter
    {
        public bool IsBoring(string word);
    }
}