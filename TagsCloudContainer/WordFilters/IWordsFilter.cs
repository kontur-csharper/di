using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}