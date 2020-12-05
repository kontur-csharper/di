using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.TagsCloudContainer;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class TagsVisualizer : RectangleVisualizer
    {
        public Bitmap GetBitmap(List<Tag> tags, Color backgroundColor)
        {
            var imageSize = GetImageSize(tags.Select(x => x.Rectangle));
            var pen = new Pen(Color.MediumVioletRed, 4);
            var bitmap = new Bitmap(imageSize.Width + (int) pen.Width, imageSize.Height + (int) pen.Width);
            using var graphics = Graphics.FromImage(bitmap);
            {
                graphics.Clear(backgroundColor);

                foreach (var tag in tags)
                    graphics.DrawString(tag.Text, tag.Font, tag.TextColor, tag.Rectangle.X, tag.Rectangle.Y);
            }

            return bitmap;
        }
    }
}