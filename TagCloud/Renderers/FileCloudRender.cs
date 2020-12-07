using System.Drawing;
using TagCloud.Settings;
using TagCloud.TagClouds;
using TagCloud.Visualizers;

namespace TagCloud.Renderers
{
    public class FileCloudRender : IRender
    {
        private readonly IVisualizer<RectangleTagCloud> visualizer;
        private readonly ResultSettings settings;

        public FileCloudRender(IVisualizer<RectangleTagCloud> visualizer, ResultSettings settings)
        {
            this.visualizer = visualizer;
            this.settings = settings;
        }

        public void Render()
        {
            var leftUpBound = visualizer.VisualizeTarget.LeftUpBound;
            var rightDownBound = visualizer.VisualizeTarget.RightDownBound;

            var image = new Bitmap(
                rightDownBound.X - leftUpBound.X,
                rightDownBound.Y - leftUpBound.Y);
            var graphics = Graphics.FromImage(image);
            visualizer.Draw(graphics);
            image.Save($"{settings.FileName}.png");
        }
    }
}
