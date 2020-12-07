using System.Collections.Generic;

namespace TagsCloud.WordFilters
{
    interface IWordFilter
    {
        public IReadOnlyList<string> FilterWords(IEnumerable<string> words);
    }
}