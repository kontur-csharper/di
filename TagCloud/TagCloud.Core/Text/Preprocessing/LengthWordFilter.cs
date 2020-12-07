using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.Text.Preprocessing
{
    public class LengthWordFilter : IWordFilter
    {
        public IEnumerable<string> GetValidWordsOnly(IEnumerable<string> words) =>
            words.Where(word => word.Length >= 3);
    }
}