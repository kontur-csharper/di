using System.Collections.Generic;

namespace TagCloud.Core.Text.Preprocessing
{
    public interface IWordFilter
    {
        IEnumerable<string> GetValidWordsOnly(IEnumerable<string> word);
    }
}