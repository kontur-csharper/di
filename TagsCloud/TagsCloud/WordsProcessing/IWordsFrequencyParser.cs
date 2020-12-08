using System.Collections.Generic;

namespace TagsCloud.WordsProcessing
{
    public interface IWordsFrequencyParser
    {
        Dictionary<string, int> ParseWordsFrequencyFromFile(string fileName);
    }
}