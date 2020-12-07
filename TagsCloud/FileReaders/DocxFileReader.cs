using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;

namespace TagsCloud.FileReaders
{
    public class DocxFileReader : IFileReader
    {
        public IReadOnlyCollection<string> GetWordsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("File not exists");

            var document = WordprocessingDocument.Open(filePath, false);

            var documentBody = document.MainDocumentPart.Document.Body;

            return documentBody
                .Select(item => item.InnerText)
                .Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
    }
}
