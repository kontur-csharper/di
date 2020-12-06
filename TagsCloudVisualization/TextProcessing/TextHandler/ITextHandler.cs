using System.Collections.Generic;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualization.TextProcessing.TextHandler
{
    public interface ITextHandler
    {
        IEnumerable<Word> GetHandledWords(string text);
    }
}