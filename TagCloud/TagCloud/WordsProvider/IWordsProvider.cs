using System.Collections.Generic;

namespace TagCloud.WordsProvider
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
    }
}