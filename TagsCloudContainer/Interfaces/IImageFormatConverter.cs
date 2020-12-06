using System.Drawing.Imaging;

namespace TagsCloudContainer.Interfaces
{
    public interface IImageFormatConverter
    {
        ImageFormat ConvertToImageFormat(string imageFormatFromString);
    }
}