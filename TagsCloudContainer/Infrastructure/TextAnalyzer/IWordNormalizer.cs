namespace TagsCloudContainer.Infrastructure.TextAnalyzer
{
    public interface IWordNormalizer
    {
        public string NormalizeWord(string word);
    }
}