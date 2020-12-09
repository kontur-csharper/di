using System.Drawing.Imaging;

namespace TagsCloudContainer.Infrastructure.Settings
{
    interface IOutputSettingsHolder
    {
        public string OutputFilePath { get; }
        public ImageFormat ImageFormat { get; }
    }
}
