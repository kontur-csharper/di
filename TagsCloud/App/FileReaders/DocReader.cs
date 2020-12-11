using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Word;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.FileReaders
{
    public class DocReader : IFileAllLinesReader
    {
        public HashSet<string> Extensions { get; } = new HashSet<string> {".doc", ".docx"};

        public string[] ReadAllLines(string filePath)
        {
            var app = new Application();
            var doc = app.Documents.Open(Path.Combine(Directory.GetCurrentDirectory(), filePath));
            var lines = doc.Paragraphs.Cast<Paragraph>().Select(x => x.Range.Text).ToArray();
            doc.Close();
            app.Quit();
            return lines;
        }
    }
}