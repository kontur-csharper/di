using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Settings;
using TagCloud.Sources;

namespace TagCloud.Extensions
{
    public static class ISourceExtensions
    {
        public static Dictionary<string, double> GetWordsWithWeight(this ISource source)
        {
            var words = new Dictionary<string, double>();
            foreach (var word in source.Words())
                if (words.TryGetValue(word, out var value))
                    words[word] = value + 1;
                else
                    words[word] = 1;

            return words;
        }

        public static ISource SelectAppropriateSourceForExtension(this ISource[] sources, SourceSettings settings)
        {
            var extension = Path.GetExtension(settings.Destination)?.Substring(1);
            return sources.FirstOrDefault(source => source.SupportExtension == extension);
        }
    }
}
