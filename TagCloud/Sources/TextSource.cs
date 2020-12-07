using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Settings;

namespace TagCloud.Sources
{
    public class TextSource : ISource
    {
        private readonly SourceSettings settings;

        public TextSource(SourceSettings settings)
        {
            this.settings = settings;
        }

        public string SupportExtension { get; } = "txt";

        public IEnumerable<string> Words()
        {
            var words = File.ReadAllLines(settings.Destination);
            var ignore = new HashSet<string>(settings.Ignore);
            foreach (var word in words.Select(w => w.ToLower()))
            {
                if (string.IsNullOrEmpty(word) || ignore.Contains(word))
                    continue;
                yield return word;
            }
        }
    }
}
