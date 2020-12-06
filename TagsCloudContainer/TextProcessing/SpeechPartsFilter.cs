using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class SpeechPartsFilter : IWordsFilter
    {
        private readonly INormalizedWordAndSpeechPartParser _normalizedWordAndSpeechPartParser;
        private readonly HashSet<string> _boringSpeechParts;

        public SpeechPartsFilter(INormalizedWordAndSpeechPartParser normalizedWordAndSpeechPartParser,
            string[] boringSpeechPartsByMyStem)
        {
            _normalizedWordAndSpeechPartParser = normalizedWordAndSpeechPartParser;
            _boringSpeechParts = boringSpeechPartsByMyStem.ToHashSet();
        }

        public string[] GetInterestingWords(string[] words)
        {
            if (words == null)
                throw new ArgumentException("Array must be not null");
            return words
                .Select(word => _normalizedWordAndSpeechPartParser.ParseToNormalizedWordAndPartSpeech(word))
                .Where(wordAndPartSpeech =>
                    wordAndPartSpeech.Length != 0 && !_boringSpeechParts.Contains(wordAndPartSpeech[1]))
                .Select(wordAndPartSpeech => wordAndPartSpeech[0])
                .ToArray();
        }
    }
}