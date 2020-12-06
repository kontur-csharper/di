using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProviders
{
    public interface IWordProvider
    {
        List<string> GetWords(string filepath);
    }
}