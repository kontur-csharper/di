using System.Collections.Generic;
using TagsCloudCreating.Core.WordProcessors;

namespace TagsCloudCreating.Contracts
{
    public interface ITagsCloudCreator
    {
        public IEnumerable<Tag> CreateTagsCloud(IEnumerable<string> words);
    }
}