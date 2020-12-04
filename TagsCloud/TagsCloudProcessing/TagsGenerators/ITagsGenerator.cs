using System.Collections.Generic;
using TagsCloud.TextProcessing;

namespace TagsCloud.TagsCloudProcessing.TagsGenerators
{
    public interface ITagsGenerator
    {
        IEnumerable<Tag> CreateTags(IEnumerable<WordInfo> words);
    }
}
