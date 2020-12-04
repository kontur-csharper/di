using System.Drawing;
using TagsCloudContainer.Common;
using TagsCloudContainer.TextAnalyzing;

namespace TagsCloudContainer
{
    public class Painter
    {
        private readonly ColorSettingsProvider colorSettingsProvider;
        private readonly IImageHolder imageHolder;
        private readonly TagCreator tagCreator;

        public Painter(ColorSettingsProvider colorSettingsProvider, IImageHolder imageHolder, TagCreator tagCreator)
        {
            this.imageHolder = imageHolder;
            this.colorSettingsProvider = colorSettingsProvider;
            this.tagCreator = tagCreator;
        }

        public void PaintTag(Tag tag, Graphics graphics)
        {
            graphics.DrawString(tag.Text, tag.Font, new SolidBrush(colorSettingsProvider.ColorSettings.GetNextColor()),
                tag.Rectangle);
            graphics.Save();
        }

        public void PaintTags()
        {
            var tags = tagCreator.GetTagsForVisualization();
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.Clear(colorSettingsProvider.ColorSettings.BackgroundColor);
                foreach (var tag in tags) PaintTag(tag, graphics);
            }

            imageHolder.UpdateUi();
        }
    }
}