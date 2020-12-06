using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.WordsProcessing.WordsFilters
{
    public class LengthFilter : IWordFilter
    {
        private readonly WordsSettings wordsSettings;

        public LengthFilter(WordsSettings wordsSettings)
        {
            this.wordsSettings = wordsSettings;
        }
        
        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word =>
                word.Length >= wordsSettings.MinLength && word.Length <= wordsSettings.MaxLength);
        }
    }
}