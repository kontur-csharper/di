using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.ColoringAlgorithms;

namespace TagCloud.Core.ImageCreators
{
    public class TagCloudImageCreator : IImageCreator
    {
        private readonly Color backgroundColor = Color.Azure;

        public Bitmap Create(IColoringAlgorithm coloringAlgorithm,
            IEnumerable<Tag> tags, string fontName, Size size)
        {
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            using var backgroundBrush = new SolidBrush(backgroundColor);
            graphics.FillRectangle(backgroundBrush,
                new Rectangle(0, 0, size.Width, size.Height));

            foreach (var tag in tags)
            {
                using var font = new Font(fontName, tag.FontSize);
                using var currentColor = new SolidBrush(coloringAlgorithm.GetNextColor(tag));
                graphics.DrawString(tag.Word, font, currentColor, tag.Rectangle);
            }

            return bitmap;
        }
    }
}