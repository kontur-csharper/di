using System.Collections.Generic;

namespace TagsCloud.WordsProcessing
{
    public interface IWordsFilter
    {
        IEnumerable<string> GetCorrectWords(IEnumerable<string> words);
    }
}