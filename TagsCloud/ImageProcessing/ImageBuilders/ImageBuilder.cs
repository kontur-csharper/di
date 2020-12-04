using System.Collections.Generic;
using System.Drawing;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.TagsCloudProcessing;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.ImageProcessing.ImageBuilders
{
    public class ImageBuilder : IImageBuilder
    {
        private readonly ImageConfig imageConfig;
        private readonly WordConfig wordsConfig;

        public ImageBuilder(ImageConfig imageConfig, WordConfig wordsConfig)
        {
            this.imageConfig = imageConfig;
            this.wordsConfig = wordsConfig;
        }

        public Bitmap BuildImage(IEnumerable<Tag> tags)
        {
            var image = new Bitmap(imageConfig.ImageSize.Width, imageConfig.ImageSize.Height);

            using var graphics = Graphics.FromImage(image);
            using var brush = new SolidBrush(wordsConfig.Color);

            foreach (var tag in tags)
                graphics.DrawString(tag.Value, tag.Font, brush, tag.Rectangle);

            return image;
        }
    }
}
