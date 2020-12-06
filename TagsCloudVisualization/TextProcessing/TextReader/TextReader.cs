using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.TextProcessing.Readers;

namespace TagsCloudVisualization.TextProcessing.TextReader
{
    public class TextReader : ITextReader
    {
        private readonly List<IReader> readers;
        
        public TextReader(IEnumerable<IReader> readers)
        {
            this.readers = readers.ToList();
        }

        public string ReadAllText(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"File {path} does not exist");

            return GetTextFromReader(path);
        }

        private string GetTextFromReader(string path)
        {
            var reader = readers.FirstOrDefault(rdr => rdr.CanReadFile(path));
            if (reader == null)
                throw new IOException($"Extension {Path.GetExtension(path)} doesn't support");

            return reader.ReadText(path);
        }
    }
}