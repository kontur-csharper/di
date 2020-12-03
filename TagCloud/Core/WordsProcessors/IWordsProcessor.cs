using System.Collections.Generic;

namespace TagCloud.Core.WordsProcessors
{
    public interface IWordsProcessor
    {
        IEnumerable<string> Process(IEnumerable<string> words, int amountToTake);
    }
}