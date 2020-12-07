using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloud.App
{
    public abstract class FileReader : IFileReader
    {
        protected Regex splitRegex = new Regex("\\W+");
        public virtual HashSet<string> AvailableFileTypes { get; }

        public IEnumerable<string> ReadWords(string fileName)
        {
            var fileType = fileName.Split('.')[^1];
            if (!AvailableFileTypes.Contains(fileType))
                throw new ArgumentException($"Incorrect type {fileType}");

            return ReadWordsInternal(fileName);
        }

        protected abstract IEnumerable<string> ReadWordsInternal(string fileName);
    }
}