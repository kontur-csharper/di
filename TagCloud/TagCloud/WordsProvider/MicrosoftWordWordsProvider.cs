using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;

namespace TagCloud.WordsProvider
{
    public class MicrosoftWordWordsProvider : FileWordsProvider
    {
        public MicrosoftWordWordsProvider(string filePath) : base(filePath)
        {
            SupportedExtensions = new[] {"doc", "docx"};
            if (!CheckFile(filePath))
                throw new ArgumentException("File is incorrect");
        }

        public override string[] SupportedExtensions { get; }

        public override IEnumerable<string> GetWords()
        {
            var application = new Application();
            var document = application.Documents.Open(FilePath);
            var words = (from Range word in document.Words select word.Text).ToList();
            application.Quit();
            return words;
        }
    }
}