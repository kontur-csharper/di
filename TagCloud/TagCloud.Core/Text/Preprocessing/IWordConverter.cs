using System.Collections.Generic;

namespace TagCloud.Core.Text.Preprocessing
{
    public interface IWordConverter
    {
        IEnumerable<string> Normalize(IEnumerable<string> words);
    }
}