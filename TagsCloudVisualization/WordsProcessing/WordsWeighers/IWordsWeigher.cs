using System.Collections.Generic;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualization.WordsProcessing.WordsWeighers
{
    public interface IWordsWeigher
    {
        IEnumerable<Word> WeighWords(IEnumerable<string> words);
    }
}