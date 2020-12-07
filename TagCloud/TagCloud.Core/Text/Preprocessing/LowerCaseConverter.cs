using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.Text.Preprocessing
{
    public class LowerCaseConverter : IWordConverter
    {
        public IEnumerable<string> Normalize(IEnumerable<string> words) => words.Select(x => x.ToLower());
    }
}