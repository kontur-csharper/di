using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class StopWordsFilter : IWordsFilter
    {
        public StopWords StopWords { get; set; }

        public StopWordsFilter(StopWords stopWords)
        {
            StopWords = stopWords;
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower()).Where(word => !StopWords.Contains(word)).ToList();
        }
    }
}
