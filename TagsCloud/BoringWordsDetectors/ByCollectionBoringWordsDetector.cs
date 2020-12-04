using System.Collections.Generic;

namespace TagsCloud.BoringWordsDetectors
{
    public class ByCollectionBoringWordsDetector : IBoringWordsDetector
    {
        private static readonly HashSet<string> BoringWords = new HashSet<string>
        {
            "я", "мы", "ты", "вы", "они", "он", "она",
            "а", "и", "но",
            "we", "i", "you", "they",
            "and", "but", "then",
        };
        
        public bool IsBoring(string word) => BoringWords.Contains(word);
    }
}