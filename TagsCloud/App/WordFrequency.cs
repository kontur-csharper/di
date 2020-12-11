using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class WordFrequency
    {
        private readonly IWordChecker wordChecker;

        public WordFrequency(IWordChecker wordChecker)
        {
            this.wordChecker = wordChecker;
        }

        public Dictionary<string, double> Get(string[] lines) =>
            lines
                .Select(x => x.ToLower().Trim())
                .Where(x => wordChecker.IsWordNotBoring(x))
                .GroupBy(x => x)
                .ToDictionary(x => x.Key,
                    x => Math.Round((double) x.Count() / lines.Length, 2));
    }
}