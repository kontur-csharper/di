using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Extensions
{
    public static class IEnumerableExtensions
    {
        internal static IEnumerable<(T item, int frequency)> MostFrequent<T>(
            this IEnumerable<T> source)
        {
            return source
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count())
                .OrderByDescending(pair => pair.Value)
                .Select(pair => (pair.Key, pair.Value));
        }
    }
}