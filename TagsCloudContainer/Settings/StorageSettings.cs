using System.Drawing.Imaging;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class StorageSettings : IStorageSettings
    {
        public string PathToCustomText { get; }
        public string PathToSave { get; }
        public ImageFormat ImageFormat { get; }

        public StorageSettings(string pathToCustomText, string pathToSave, ImageFormat imageFormat)
        {
            PathToCustomText = pathToCustomText;
            PathToSave = pathToSave;
            ImageFormat = imageFormat;
        }
    }
}