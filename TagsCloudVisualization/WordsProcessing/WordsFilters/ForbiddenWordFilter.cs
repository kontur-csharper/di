using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.WordsProcessing.WordsFilters
{
    public class ForbiddenWordFilter : IWordFilter
    {
        private readonly WordsSettings wordsSettings;

        public ForbiddenWordFilter(WordsSettings wordsSettings)
        {
            this.wordsSettings = wordsSettings;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            var forbiddenWords = wordsSettings.GetForbiddenWords;

            return words.Where(word => !forbiddenWords.Contains(word));
        }
    }
}