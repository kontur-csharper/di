using System.Collections.Generic;

namespace TagsCloud.StatisticProviders
{
    public interface IStatisticProvider
    {
        Dictionary<string, int> GetWordStatistics(IEnumerable<string> words);
    }
}