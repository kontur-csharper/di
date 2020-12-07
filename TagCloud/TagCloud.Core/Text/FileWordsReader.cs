using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagCloud.Core.Text
{
    public class FileWordsReader : IFileWordsReader
    {
        private static readonly Regex wordsRegex = new Regex(
            @"(?<word>\w+)\W?",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string[] GetWordsFrom(string path)
        {
            var text = File.ReadAllText(path);
            var allWords = new List<string>();
            var match = wordsRegex.Match(text);
            while (match.Success)
            {
                allWords.Add(string.Intern(match.Groups["word"].Value));
                match = match.NextMatch();
            }

            return allWords.ToArray();
        }
    }
}