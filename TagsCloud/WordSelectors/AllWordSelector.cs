using System.Collections.Generic;

namespace TagsCloud.WordSelectors
{
    public class AllWordSelector : IWordSelector
    {
        public IEnumerable<string> TakeSelectedWords(IEnumerable<string> words)
        {
            return words;
        }
    }
}