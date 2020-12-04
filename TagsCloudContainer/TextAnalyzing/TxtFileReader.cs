using System.IO;

namespace TagsCloudContainer.TextAnalyzing
{
    internal class TxtFileReader : ITextFileReader
    {
        public string GetContent(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var content = reader.ReadToEnd();
                return content;
            }
        }
    }
}