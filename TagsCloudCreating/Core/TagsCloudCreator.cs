using System.Collections.Generic;
using System.Linq;
using TagsCloudCreating.Contracts;
using TagsCloudCreating.Core.WordProcessors;

namespace TagsCloudCreating.Core
{
    public class TagsCloudCreator : ITagsCloudCreator
    {
        private ITagsCloudLayouter Layouter { get; }
        private IWordHandler WordHandler { get; }
        private WordConverter WordConverter { get; }

        public TagsCloudCreator(ITagsCloudLayouter layouter, IWordHandler wordHandler, WordConverter wordConverter)
        {
            Layouter = layouter;
            WordHandler = wordHandler;
            WordConverter = wordConverter;
        }

        public IEnumerable<Tag> CreateTagsCloud(IEnumerable<string> words)
        {
            var interestingWords = WordHandler.NormalizeAndExcludeBoringWords(words);
            var readyTags = WordConverter.ConvertToTags(interestingWords)
                .Select(InsertTagInFrame)
                .OrderByDescending(t => t.Frequency);
            Layouter.Recreate();
            return readyTags;
        }

        private Tag InsertTagInFrame(Tag tag)
        {
            var frame = Layouter.PutNextRectangle(tag.GetCeilingSize());
            return tag.InsertTagInFrame(frame);
        }
    }
}