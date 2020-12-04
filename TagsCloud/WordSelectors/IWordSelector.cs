using System.Collections.Generic;

namespace TagsCloud.WordSelectors
{
    public interface IWordSelector
    {
        IEnumerable<string> TakeSelectedWords(IEnumerable<string> words);
    }
}