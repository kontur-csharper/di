using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface IImageSizeConverter
    {
        Size ConvertToSize(int[] sizeParameters);
    }
}