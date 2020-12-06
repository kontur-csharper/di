using System.Collections.Generic;
using System.IO;
using Xceed.Words.NET;

namespace TagsCloudVisualization.TextProcessing.Readers
{
    public class MSWordReader : IReader
    {
        private readonly HashSet<string> supportingExtensions = new HashSet<string>{".doc", ".docx"};
        
        public string ReadText(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"File {path} does not exist");
            if (!CanReadFile(path))
                throw new IOException($"MSWordReader doesn't support extension {Path.GetExtension(path)}");
            
            return ReadDocument(path);
        }

        public bool CanReadFile(string path) => supportingExtensions.Contains(Path.GetExtension(path));

        private static string ReadDocument(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var document = DocX.Load(fs);
            return document.Text;
        }
    }
}