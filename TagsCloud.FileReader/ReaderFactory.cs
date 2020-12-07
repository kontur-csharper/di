using System;

namespace TagsCloud.FileReader
{
    public class ReaderFactory : IReaderFactory
    {
        public IWordsReader GetReader(string extension)
        {
            switch (extension)
            {
                case "txt":
                    return new TxtReader();
                case "rtf":
                    return new RtfReader();
                case "docx":
                    return new DocxReader();
                default:
                    throw new ArgumentException("not valid extension of file");
            }
        }
    }
}