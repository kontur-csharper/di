using System;
using TagCloud.WordsProvider;

namespace TagCloud.ConsoleAppHelper
{
    public static class WordsProviderFinder
    {
        public static Type FindFileWordsProvider(string extension)
        {
            switch (extension)
            {
                case ".txt":
                    return typeof(TxtWordsProvider);
                case ".doc":
                case ".docx":
                    return typeof(MicrosoftWordWordsProvider);
                default:
                    throw new ArgumentException("Could not find suitable words provider");
            }
        }
    }
}