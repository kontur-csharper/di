using System.Collections.Generic;
using System.Linq;
using TagCloud.Settings;
using Xceed.Words.NET;

namespace TagCloud.Sources
{
    public class DocxSource : ISource
    {
        private readonly SourceSettings sourceSettings;

        public DocxSource(SourceSettings sourceSettings)
        {
            this.sourceSettings = sourceSettings;
        }

        public string SupportExtension { get; } = "docx";

        public IEnumerable<string> Words()
        {
            var doc = DocX.Load(sourceSettings.Destination);
            var ignore = new HashSet<string>(sourceSettings.Ignore);
            foreach (var word in doc.Paragraphs.Select(p => p.Text.ToLower()))
            {
                if (string.IsNullOrEmpty(word) || ignore.Contains(word))
                    continue;
                yield return word;
            }
        }
    }
}
