using System.Collections.Generic;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer.TagsCloudContainer.Interfaces
{
    public interface ITagsContainer
    {
        public List<Tag> GetTags(string text, SpiralType type);
    }
}