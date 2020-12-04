using System.Collections.Generic;

namespace TagCloud.WordsAnalyzer
{
    public interface IWordsAnalyzer
    {
        public HashSet<TagInfo> GetTags(IReadOnlyCollection <string> words);
    }
}