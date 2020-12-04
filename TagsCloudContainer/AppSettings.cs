using TagsCloudContainer.Common;

namespace TagsCloudContainer
{
    public class AppSettings
    {
        public AppSettings(FilesSettings filesSettings, ImageSettings imageSettings)
        {
            FilesSettings = filesSettings;
            ImageSettings = imageSettings;
        }

        public FilesSettings FilesSettings { get; set; }
        public ImageSettings ImageSettings { get; set; }
    }
}