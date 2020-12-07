using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagsCloudHandler
    {
        private readonly ITagsCloudDrawer drawer;
        private readonly IWordNormalizer wordNormalizer;
        private readonly IEnumerable<IWordsFilter> wordsFilters;

        public TagsCloudHandler(
            IEnumerable<IWordsFilter> filters,
            IWordNormalizer normalizer,
            ITagsCloudDrawer drawer)
        {
            wordNormalizer = normalizer;
            wordsFilters = filters;
            this.drawer = drawer;
        }

        public Image GetNewTagcloud(IEnumerable<string> words)
        {
            var neededWords = words
                .Where(word => wordsFilters.All(filter => filter.Validate(word)))
                .Select(word => wordNormalizer.Normalize(word))
                .ToList();
            var counts = neededWords
                .GroupBy(word => word)
                .Select(group => new Word(group.Key, (double) group.Count() / neededWords.Count));

            return drawer.GetTagsCloud(counts);
        }
    }
}