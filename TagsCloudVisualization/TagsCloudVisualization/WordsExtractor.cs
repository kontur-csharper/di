﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class WordsExtractor :IWordsExtractor
    {
        private readonly WordsExtractorSettings settings;

        public WordsExtractor(WordsExtractorSettings settings)
        {
            this.settings = settings;
        }

        public List<string> Extract(string path)
        {
            var text = File.ReadAllText(path, Encoding.Default)
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Replace("\t", " ");
            text = settings.StopChars.Aggregate(text, (current, c) => current.Replace(c, ' '));
            var words = text.Split(' ')
                .Where(w => w.Length >= 3 && w != string.Empty && !settings.StopWords.Contains(w))
                .Select(w => w.Trim().ToLowerInvariant()).ToList();
            return words;
        }
    }
}
