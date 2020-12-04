using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagCloud.WordsProvider
{
    public class TxtWordsProvider : FileWordsProvider
    {
        public TxtWordsProvider(string filePath) : base(filePath)
        {
            SupportedExtensions = new[] {"txt"};
            if (!CheckFile(filePath))
                throw new ArgumentException("File is incorrect");
        }

        public override string[] SupportedExtensions { get; }

        public override IEnumerable<string> GetWords()
        {
            var words = Regex.Split(File.ReadAllText(FilePath), @"\W+");
            return words;
        }
    }
}