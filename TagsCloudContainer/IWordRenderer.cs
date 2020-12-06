using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordRenderer
    {
        IEnumerable<LayoutedWord> SizeWords(IEnumerable<LayoutedWord> words);
        void Render(IEnumerable<LayoutedWord> words);
    }
}