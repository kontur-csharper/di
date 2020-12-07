using System.Collections.Generic;

namespace TagCloud.Core.Text.Formatting
{
    public interface IFontSizeSource
    {
        FontSizeSourceType Type { get; }
        IDictionary<string, float> GetFontSizesForAll(Dictionary<string, int> allWords);
    }
}