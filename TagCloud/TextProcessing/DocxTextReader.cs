using System.Linq;
using DocumentFormat.OpenXml.Packaging;

namespace TagCloud.TextProcessing
{
    public class DocxTextReader : ITextReader
    {
        public string[] ReadStrings(string pathToFile)
        {
            var doc = WordprocessingDocument.Open(pathToFile, false);
            var body = doc.MainDocumentPart.Document.Body;

            return body.Select(item => item.InnerText).ToArray();
        }
    }
}