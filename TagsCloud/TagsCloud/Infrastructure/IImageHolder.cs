using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouters;

namespace TagsCloud.Infrastructure
{
    public interface IImageHolder
    {
        ImageSettings Settings { get; }
        void ChangeLayouter(ICloudLayouter layouter);
        void RecreateCanvas(ImageSettings settings);
        void SaveImage(string fileName);
        void RenderWordsFromFile(string fileName);
        void RedrawCurrentImage();
    }
}