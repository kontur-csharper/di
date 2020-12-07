using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.App
{
    public class BlackListWordsFilter : IWordsFilter
    {
        internal readonly HashSet<string> BlackList;
        private readonly IWordNormalizer normalizer;

        public BlackListWordsFilter(IEnumerable<string> excludedWords, IWordNormalizer normalizer)
        {
            BlackList = excludedWords.Select(normalizer.Normalize).ToHashSet();
            this.normalizer = normalizer;
        }

        public bool Validate(string word)
        {
            return !BlackList.Contains(normalizer.Normalize(word));
        }

        public void UpdateBlackList(IEnumerable<string> words)
        {
            if (words == null)
                throw new NullReferenceException("Words collection should not be null");
            BlackList.Clear();
            foreach (var word in words)
                BlackList.Add(normalizer.Normalize(word));
        }
    }
}