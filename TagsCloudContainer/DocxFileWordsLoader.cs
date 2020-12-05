using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagsCloudContainer
{
    public class DocxFileWordsLoader : FileWordsLoader
    {
        protected override string[] SupportedFormats { get; } = {".docx"};

        public DocxFileWordsLoader(string pathToFile)
            : base(pathToFile)
        {
        }

        public override string[] GetWords()
        {
            var words = new List<string>();
            using (var wordDocument = WordprocessingDocument.Open(pathToFile, false))
            {
                var paragraphs = wordDocument.MainDocumentPart.Document.Body.OfType<Paragraph>();
                words.AddRange(paragraphs.Select(paragraph => paragraph.InnerText));
            }

            return words.ToArray();
        }
    }
}