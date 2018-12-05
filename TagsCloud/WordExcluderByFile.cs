﻿using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class WordExcluderByFile : IWordExcluder
    {
        private readonly IEnumerable<string> stopWords;

        public WordExcluderByFile(IFileReader fileReader, string path)
        {
            fileReader.Path = path;
            stopWords = fileReader.Read();
        }

        public bool ToExclude(string word)
        {
            return stopWords.Contains(word);
        }
    }
}