using System;
using System.Linq;
using WeCantSpell.Hunspell;

namespace TagCloud.TextProcessing
{
    public class LiteratureTextParser : IWordParser
    {
        private readonly IPathCreater creater;
        private readonly ITextReader reader;
        private static readonly char[] separators = {' ', '.', ',', ':', '!'};
        private const int minWordLength = 3;
        private static string[] unneccesaryWords = 
            {"мочь", "этот", "когда", "чтобы", "даже", "между", "если", "несколько", "который", "какой", "только",
                "очень", "более", "ничто", "кто", "он", "такой", "однако", "либо", "оный", "такой", "него"};
        
            
        public LiteratureTextParser(IPathCreater creater, ITextReader reader)
        {
            this.creater = creater;
            this.reader = reader;
        }
        
        public string[] GetWords(string inputFileName)
        {
            var path = creater.GetCurrentPath();
            var dictionary = GetDictionary(path);
            var lines = reader.ReadStrings(path + inputFileName);

            return lines
                .SelectMany(line => line.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                .Select(word => word.ToLower())
                .Select(word => GetRootForWord(word, dictionary))
                .Where(word => !(word is null))
                .Where(word => word.Length > minWordLength)
                .Where(word => !unneccesaryWords.Contains(word))
                .ToArray();
        }

        private static string GetRootForWord(string word, WordList dictionary)
        {
            return dictionary.ContainsEntriesForRootWord(word) ? word : dictionary.CheckDetails(word).Root;
        }

        private static WordList GetDictionary(string path)
        {
            return WordList.CreateFromFiles(path + "ru_RU.dic", path + "ru_RU.aff");
        }
    }
}