using TagsCloudContainer.Infrastructure.TextAnalyzer;

namespace TagsCloudContainer.App.TextAnalyzer
{
    internal class ToLowerWordNormalizer : IWordNormalizer
    {
        public string NormalizeWord(string word)
        {
            return word.ToLower();
        }
    }
}