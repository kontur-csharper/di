using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class WordsNormalizer : IWordsNormalizer
    {
        public List<string> NormalizeWords(List<string> words, HashSet<string> boringWords) =>
            words.Select(x => x.ToLower()).Where(x => !boringWords.Contains(x)).ToList();
    }
}