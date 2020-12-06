using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualization.WordsProcessing.WordsWeighers
{
    public class FrequencyWordWeigher : IWordsWeigher
    {
        public IEnumerable<Word> WeighWords(IEnumerable<string> words)
        {
            return words
                .GroupBy(word => word)
                .Select(x => new Word(x.Key, x.Count()))
                .OrderByDescending(word => word.Weight);
        }
    }
}