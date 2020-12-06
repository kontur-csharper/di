using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProcessing.WordsFilters
{
    public interface IWordFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}