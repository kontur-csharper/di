using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordsFrequency : IWordsFrequency
    {
        private readonly ITextConverter _textConverter;
        private readonly IEnumerable<IWordsFilter> _wordsFilters;

        public WordsFrequency(ITextConverter textConverter, IEnumerable<IWordsFilter> wordsFilters)
        {
            _textConverter = textConverter;
            _wordsFilters = wordsFilters;
        }

        public Dictionary<string, int> GetWordsFrequency(string text)
        {
            if (text == null)
                throw new ArgumentException("String must be not null");
            var words = _textConverter.ConvertTextToCertainFormat(text).Split(Environment.NewLine);
            return _wordsFilters
                .Aggregate(words, (current, wordsFilter) => wordsFilter.GetInterestingWords(current))
                .GroupBy(word => word)
                .ToDictionary(wordsGroup => wordsGroup.Key,
                    wordsGroup => wordsGroup.Count());
        }
    }
}