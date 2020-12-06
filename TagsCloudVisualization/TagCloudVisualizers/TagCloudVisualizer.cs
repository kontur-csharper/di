using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.Canvases;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.TagCloudVisualizers
{
    public class TagCloudVisualizer : ICloudVisualizer
    {
        private readonly PaintingSettings paintingSettings;
        private readonly ICanvas canvas;

        public TagCloudVisualizer(ICanvas canvas, PaintingSettings paintingSettings)
        {
            this.canvas = canvas;
            this.paintingSettings = paintingSettings;
        }

        public void PrintTagCloud(IEnumerable<Tag> tags)
        {
            var imageSize = canvas.GetImageSize();
            using (var graphics = canvas.StartDrawing())
            {
                using (var backgroundBrush = new SolidBrush(paintingSettings.BackgroundColor))
                    graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);

                using (var textBrush = new SolidBrush(paintingSettings.TextColor)) 
                    DrawCloud(tags, textBrush, graphics);
            }
            
            canvas.UpdateUi();
        }

        private void DrawCloud(IEnumerable<Tag> tags, SolidBrush textBrush, Graphics graphics)
        {
            var pen = new Pen(Color.White);
            var iterationCount = 0;
            foreach (var tag in tags)
            {
                if (paintingSettings.EnabledMultipleColors)
                    textBrush.Color = paintingSettings.Colors[iterationCount % paintingSettings.Colors.Length];
                
                graphics.DrawString(tag.Text, tag.Font, textBrush, tag.BoundingZone.Location);

                if (paintingSettings.DrawTextBoundingZone)
                {
                    pen.Color = Color.Beige;
                    graphics.DrawRectangle(pen, tag.BoundingZone);
                }

                iterationCount++;
                if (paintingSettings.EnableAnimation)
                    canvas.UpdateUi();
            }
        }
    }
}