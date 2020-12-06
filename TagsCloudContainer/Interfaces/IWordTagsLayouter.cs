using System.Collections.Generic;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordTagsLayouter
    {
        (IReadOnlyList<WordTag>, int) GetWordTagsAndCloudRadius(string text);
    }
}