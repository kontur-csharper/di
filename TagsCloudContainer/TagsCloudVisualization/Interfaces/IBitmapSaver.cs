using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Interfaces
{
    public interface IBitmapSaver
    {
        public void SaveBitmapToDirectory(Bitmap imageBitmap, string savePath);
    }
}