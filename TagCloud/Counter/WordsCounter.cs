﻿using System.Collections.Generic;
using System.Linq;
using TagCloud.Data;

namespace TagCloud.Counter
{
    public class WordsCounter : IWordsCounter
    {
        public IEnumerable<WordInfo> Count(IEnumerable<string> words)
        {
            var occurrences = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (occurrences.ContainsKey(word))
                    occurrences[word]++;
                else
                    occurrences[word] = 1;
            }
            return occurrences
                .Select(pair => new WordInfo(pair.Key, pair.Value))
                .OrderByDescending(info => info.Occurrences);
        }
    }
}