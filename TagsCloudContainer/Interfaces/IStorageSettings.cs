using System.Drawing.Imaging;

namespace TagsCloudContainer.Interfaces
{
    public interface IStorageSettings
    {
        string PathToCustomText { get; }
        string PathToSave { get; }

        ImageFormat ImageFormat { get; }
    }
}