using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class Visualization : IVisualization
    {
        private readonly IVisualizationSettings _visualizationSettings;

        public Visualization(IVisualizationSettings visualizationSettings)
        {
            _visualizationSettings = visualizationSettings;
        }

        public Image GetImageCloud(IReadOnlyList<WordTag> tags, int cloudRadius)
        {
            var realImageSize = new Size(cloudRadius * 2, cloudRadius * 2);
            if (realImageSize.Height <= _visualizationSettings.ImageSize.Height &&
                realImageSize.Width <= _visualizationSettings.ImageSize.Width)
                realImageSize = _visualizationSettings.ImageSize;
            if (!_visualizationSettings.ImageSize.IsEmpty)
                Console.WriteLine("Real cloud sizes are used, but custom ones don't fit");
            var bitmap = new Bitmap(realImageSize.Width, realImageSize.Height);
            DrawCloud(bitmap, tags, realImageSize);
            return bitmap;
        }

        private void DrawCloud(Image image, IReadOnlyList<WordTag> tags, Size imageSize)
        {
            using var graphics = Graphics.FromImage(image);
            graphics.Clear(_visualizationSettings.BackgroundColor);
            foreach (var tag in tags)
            {
                var rectangle = tag.Rectangle;
                rectangle.Offset(imageSize.Width / 2, imageSize.Height / 2);
                graphics.DrawString(tag.Text, tag.WordFont,
                    new SolidBrush(_visualizationSettings.TextColor), rectangle);
            }

            graphics.Flush();
        }
    }
}