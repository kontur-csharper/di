using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IWordsForCloudGenerator
    {
        List<WordForCloud> Generate(List<string> words);
    }
}