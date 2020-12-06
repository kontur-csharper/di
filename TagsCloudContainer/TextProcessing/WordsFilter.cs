using System;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordsFilter : IWordsFilter
    {
        private readonly ITextProcessingSettings _textProcessingSettings;

        public WordsFilter(ITextProcessingSettings textProcessingSettings)
        {
            _textProcessingSettings = textProcessingSettings;
        }

        public string[] GetInterestingWords(string[] words)
        {
            if (words == null)
                throw new ArgumentException("Array must be not null");
            return words.Where(word => !_textProcessingSettings.BoringWords.Contains(word)).ToArray();
        }
    }
}