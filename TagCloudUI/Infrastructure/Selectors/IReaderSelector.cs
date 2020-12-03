using TagCloud.Core.FileReaders;

namespace TagCloudUI.Infrastructure.Selectors
{
    public interface IReaderSelector
    {
        bool TryGetReader(FileExtension extension, out IFileReader reader);
    }
}