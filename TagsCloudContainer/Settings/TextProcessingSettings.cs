using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class TextProcessingSettings : ITextProcessingSettings
    {
        public HashSet<string> BoringWords { get; }

        public TextProcessingSettings(string[] boringWords)
        {
            BoringWords = boringWords.ToHashSet();
        }
    }
}