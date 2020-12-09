using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.Settings
{
    public class ImageSizeSettings : IImageSizeSettingsHolder
    {
        public static readonly ImageSizeSettings Instance = new ImageSizeSettings();

        private ImageSizeSettings()
        {
            Width = 500;
            Height = 500;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}