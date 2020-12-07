using System.Collections.Generic;

namespace TagCloud.Sources
{
    public interface ISource
    {
        string SupportExtension { get; }
        IEnumerable<string> Words();
    }
}
