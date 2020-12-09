using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Infrastructure.DataReader;

namespace TagsCloudContainer.App.DataReader
{
    class WordFileReader : IDataReader
    {
        private readonly string filename;

        public WordFileReader(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<string> ReadLines()
        {
            return WordprocessingDocument
                .Open(filename, false)
                .MainDocumentPart
                .Document
                .Body.Select(item => item.InnerText);
        }
    }
}
