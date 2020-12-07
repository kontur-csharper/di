using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Utils;

namespace TagCloud.Core.Text.Formatting
{
    public class RandomFontSizeSource : IFontSizeSource
    {
        private static readonly int[] sizes = Enumerable.Range(5, 15)
            .Select(i => i * 2)
            .ToArray();

        public FontSizeSourceType Type => FontSizeSourceType.Random;

        public IDictionary<string, float> GetFontSizesForAll(Dictionary<string, int > words) =>
            words.ToDictionary(w => w.Key, _ => (float) Randomized.ItemFrom(sizes));
    }
}