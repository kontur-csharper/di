using System.Collections.Generic;
using System.Linq;
using TagsCloudCreating.Configuration;
using TagsCloudCreating.Contracts;

namespace TagsCloudCreating.Core.WordProcessors
{
    public class WordHandler : IWordHandler
    {
        private WordHandlerSettings WordHandlerSettings { get; }

        public WordHandler(WordHandlerSettings wordHandlerSettings) => WordHandlerSettings = wordHandlerSettings;

        public IEnumerable<string> NormalizeAndExcludeBoringWords(IEnumerable<string> words)
        {
            var boringTypes = WordHandlerSettings.SpeechPartsStatuses
                .Where(part => !part.Value)
                .Select(part => MyStemHandler.BoringWords[part.Key])
                .ToHashSet();

            return MyStemHandler.GetWordsWithPartsOfSpeech(words)
                .Where(pair => !string.IsNullOrEmpty(pair.word)
                               && !boringTypes.Contains(pair.speechPart)
                               && pair.word.Length > 1
                )
                .Select(pair => pair.word);
        }
    }
}