using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.Text.Formatting
{
    public class FontSizeSourceResolver : IFontSizeSourceResolver
    {
        private readonly Dictionary<FontSizeSourceType, IFontSizeSource> fontSizeSources;

        public FontSizeSourceResolver(IEnumerable<IFontSizeSource> sources)
        {
            fontSizeSources = sources.ToDictionary(s => s.Type);
        }

        public IFontSizeSource Get(FontSizeSourceType type) => fontSizeSources[type];
    }
}