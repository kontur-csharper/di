using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsLayouter
    {
        IEnumerable<LayoutedWord> LayoutWords(IEnumerable<LayoutedWord> words);
    }
}