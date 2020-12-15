using System.Collections.Generic;

namespace TagCloud.TextConverters.TextReaders
{
    public static class TextReaderAssosiation
    {
        private static readonly Dictionary<string, ITextReader> textReaders =
            new Dictionary<string, ITextReader>
            {
                [".txt"] = new TextReaderTxt()
            };

        public static ITextReader GetTextReader(string path) =>
            textReaders.TryGetValue(path[path.LastIndexOf('.')..path.Length], out var reader) ? reader : null;
    }
}
