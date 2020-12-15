using System.Collections.Generic;
using System.Linq;
using TagsCloudCreating.Configuration;

namespace TagsCloudCreating.Core.WordProcessors
{
    public class WordConverter
    {
        private TagsSettings TagsSettings { get; }
        public WordConverter(TagsSettings tagsSettings) => TagsSettings = tagsSettings;

        public IEnumerable<Tag> ConvertToTags(IEnumerable<string> words)
        {
            var frequencyTable = new Dictionary<string, int>();

            foreach (var word in words)
                frequencyTable[word] = frequencyTable.TryGetValue(word, out var wordStat)
                    ? wordStat + 1
                    : 1;

            return frequencyTable.Select(pair => new Tag(pair.Key, pair.Value, TagsSettings));
        }
    }
}