using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface ITextProcessingSettings
    {
        HashSet<string> BoringWords { get; }
    }
}