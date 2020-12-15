using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using TagCloud.PointGetters;
using System.Collections;
using TagCloud.CloudLayoters;

namespace TagCloud
{
    public class TagCloud : IEnumerable<(string word, Rectangle location)>
    {
        internal readonly ICloudLayoter layouter;
        private readonly Dictionary<string, double> wordsMetric;
        private readonly (string word, Rectangle location)[] locations;
        public int Top => layouter.Top;
        public int Bottom => layouter.Bottom;
        public int Left => layouter.Left;
        public int Right => layouter.Right;
        public Point Center => layouter.Center();

        internal TagCloud(Dictionary<string, double> wordsMetric, ICloudLayoter layoter)
        {
            this.wordsMetric = wordsMetric;
            this.layouter = layoter;
            locations = GetLocations();
        }

        public IEnumerator<(string word, Rectangle location)> GetEnumerator()
        {
            foreach (var w in locations)
                yield return w;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private (string word, Rectangle location)[] GetLocations() =>
            wordsMetric
            .OrderByDescending(w => w.Value)
            .Select(w => GetWordLocation(w))
            .ToArray();

        private (string word, Rectangle location) GetWordLocation(KeyValuePair<string, double> word)
        {
            var size = new Size(10 * word.Key.Length * (int)word.Value, 10 * (int)word.Value);
            var location = layouter.PutNextRectangle(size);
            return (word.Key, location);
        }
    }
}
