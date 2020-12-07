using System.Collections.Generic;
using System.Linq;
using MyStem.Wrapper.Workers.Lemmas;

namespace TagCloud.Core.Text.Preprocessing
{
    public class MyStemWordsConverter : IWordConverter
    {
        private readonly ILemmatizer normalizer;

        public MyStemWordsConverter(ILemmatizer normalizer)
        {
            this.normalizer = normalizer;
        }

        public IEnumerable<string> Normalize(IEnumerable<string> words) =>
            normalizer.GetWords(string.Join(" ", words))
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.TrimEnd('?'))
                .ToArray();
    }
}