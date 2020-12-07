using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace TagsCloud.FileReader
{
    public class DocxReader : IWordsReader
    {
        public IEnumerable<string> ReadWords(string path)
        {
            var wordDocument = WordprocessingDocument.Open(path, false);
            return wordDocument.MainDocumentPart.Document.Body.InnerText
                .Split(new string[0], StringSplitOptions.RemoveEmptyEntries);
        }
    }
}