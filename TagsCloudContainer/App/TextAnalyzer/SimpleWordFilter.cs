using TagsCloudContainer.Infrastructure.TextAnalyzer;

namespace TagsCloudContainer.App.TextAnalyzer
{
    internal class SimpleWordFilter : IWordFilter
    {
        public bool IsBoring(string word)
        {
            return word.Length < 4;
        }
    }
}