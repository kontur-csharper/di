using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.TextProcessing.Readers
{
    public class TxtReader : IReader
    {
        private readonly HashSet<string> supportingExtensions = new HashSet<string>{".txt"};
        
        public string ReadText(string path)
        {
            var extension = Path.GetExtension(path);
            if (!File.Exists(path))
                throw new IOException($"File {path} does not exist");
            if (!CanReadFile(path))
                throw new IOException($"TxtReader doesn't support extension {extension}");
            
            return File.ReadAllText(path);
        }

        public bool CanReadFile(string path) => supportingExtensions.Contains(Path.GetExtension(path));
    }
}