using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeExercise
{
    public class FileProcessor : IFileProcessor
    {
        private static char[] punctuationMarks = {'.', ',', '?', '!', ':', ':', '-', '—', '«', '»', '[', ']', '(', ')', '{','}','„','“'};
        private readonly string pathWords;
        private readonly string pathBoringWords;
        private List<string> exludedWords = new List<string>();
        
        public FileProcessor(string pathWords, string pathBoringWords)
        {
            this.pathWords = pathWords;
            this.pathBoringWords = pathBoringWords;
        }
        
        public Dictionary<string, int> GetWords()
        {
            var words = ExtractTextFromFile(pathWords);
            if(pathBoringWords!=null)
                exludedWords = ExtractTextFromFile(pathBoringWords);
            
            var formattedWords = FilterWords(words);
            
            return GetFrequencyWords(formattedWords);
        }

        private string[] FilterWords(List<string> words)
        {
            if (words == null) throw new ArgumentNullException(nameof(words));
            return words
                .Where(w=>!exludedWords.Contains(w))
                .Select(w => w.ToLower())
                .ToArray();
        }

        private Dictionary<string, int> GetFrequencyWords(IEnumerable<string> formattedWords)
        {
            return formattedWords
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .ToDictionary(grouping => grouping.Key, grouping => grouping.Count());
        }

        private List<string> ExtractTextFromFile(string path)
        {
            var result = new List<string>();
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                result.AddRange(line.Split(' ').Select(w => w.Trim(punctuationMarks)));
            }
            
            return result;
        }
    }
}