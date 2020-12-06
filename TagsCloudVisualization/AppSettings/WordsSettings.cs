using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.AppSettings
{
    public class WordsSettings
    {
        public string[] ForbiddenWords { get; set; } = new string[0];
        public int MinLength { get; set; } = 4;
        public int MaxLength { get; set; } = 10;

        public HashSet<string> GetForbiddenWords => ForbiddenWords.Select(word => word.ToLower()).ToHashSet();
    }
}