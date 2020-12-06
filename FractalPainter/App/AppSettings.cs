using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App
{
    public class AppSettings : IImageDirectoryProvider
    {
        public ImageSettings ImageSettings { get; set; }
        public string ImagesDirectory { get; set; }
    }
}