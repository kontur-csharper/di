using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.FileReaders;

namespace TagCloudUI.Infrastructure.Selectors
{
    public class ReaderSelector : IReaderSelector
    {
        private readonly Dictionary<FileExtension, IFileReader> extensionToReader;

        public ReaderSelector(IEnumerable<IFileReader> readers)
        {
            extensionToReader = readers.ToDictionary(reader => reader.Extension);
        }

        public bool TryGetReader(FileExtension extension, out IFileReader reader)
        {
            return extensionToReader.TryGetValue(extension, out reader);
        }
    }
}