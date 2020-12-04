using System.IO;
using Microsoft.Office.Interop.Word;

namespace TagsCloudContainer.TextAnalyzing
{
    internal class DocFileReader : ITextFileReader
    {
        public string GetContent(string filePath)
        {
            var wordApp = new Application();
            var fullPath = Path.GetFullPath(filePath);
            var wordDoc = wordApp.Documents.Open(fullPath, Visible: false, ReadOnly: true);
            var content = wordDoc.Content.Text;
            wordDoc.Close();
            return content;
        }
    }
}