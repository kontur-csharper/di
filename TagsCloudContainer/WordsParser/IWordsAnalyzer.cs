using System.Collections.Generic;

namespace TagsCloudContainer.WordsParser
{
    public interface IWordsAnalyzer
    {
        public Dictionary<string, int> AnalyzeWords();
    }
}