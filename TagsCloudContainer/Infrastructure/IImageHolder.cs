using System.Drawing;

namespace TagsCloudContainer.Infrastructure
{
    public interface IImageHolder
    {
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage();
        void SaveImage();
        void GenerateImage();
    }
}