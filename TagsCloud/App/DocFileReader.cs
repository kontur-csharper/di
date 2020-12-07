using System.Collections.Generic;
using System.Linq;
using MicrosoftWord = Microsoft.Office.Interop.Word;

namespace TagsCloud.App
{
    public class DocFileReader : FileReader
    {
        public override HashSet<string> AvailableFileTypes { get; } = new HashSet<string> {"doc", "docx"};

        protected override IEnumerable<string> ReadWordsInternal(string fileName)
        {
            var app = new MicrosoftWord.Application();
            var objFileName = (object) fileName;
            app.Documents.Open(ref objFileName);
            var doc = app.ActiveDocument;
            if (doc.Paragraphs.Count == 1 && doc.Paragraphs.First.Range.Text == "\r")
            {
                app.Quit();
                return new string[0];
            }

            var paragraphs = doc
                .Paragraphs
                .Cast<MicrosoftWord.Paragraph>()
                .SelectMany(p => splitRegex.Split(p.Range.Text.Trim('\r')))
                .ToArray();
            app.Quit();
            return paragraphs;
        }
    }
}