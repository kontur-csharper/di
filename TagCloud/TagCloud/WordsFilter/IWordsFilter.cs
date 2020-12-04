using System.Collections.Generic;

namespace TagCloud.WordsFilter
{
    public interface IWordsFilter
    {
        IEnumerable<string> Apply(IEnumerable<string> word);
    }
}