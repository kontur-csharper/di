using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloud.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        public IReadOnlyCollection<string> GetWordsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("File not exists");

            return File.ReadLines(filePath).Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
    }
}
