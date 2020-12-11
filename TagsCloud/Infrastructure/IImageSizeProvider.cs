using TagsCloud.App;

namespace TagsCloud.Infrastructure
{
    public interface IImageSizeProvider
    {
        ImageSize ImageSize { get; }
    }
}