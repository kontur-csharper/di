using TagsCloudVisualization.Canvases;

namespace TagsCloudVisualization.ImageSavers
{
    public interface IImageSaver
    {
        void SaveImage(ICanvas canvas, string fileName);
    }
}