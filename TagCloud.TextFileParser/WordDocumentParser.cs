using System;
using System.Collections.Generic;
using System.IO;
using GemBox.Document;

namespace TagCloud.TextFileParser
{
    public class WordDocumentParser : ITextFileParser
    {
        private const string LineSplitter = "\r\n";

        public bool TryGetWords(string fileName, string sourceFolderPath, out IEnumerable<string> result)
        {
            result = null;
            if (Path.GetExtension(fileName) != ".docx" && Path.GetExtension(fileName) != ".doc")
            {
                return false;
            }

            var document = DocumentModel.Load(Path.Combine(sourceFolderPath,
                $"{fileName}"));
            var text = document.Content.ToString();
            result = text.Split(LineSplitter, StringSplitOptions.RemoveEmptyEntries);
            return true;
        }
    }
}