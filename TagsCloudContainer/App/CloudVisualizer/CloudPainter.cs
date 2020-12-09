using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.App.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudPainter : ICloudPainter
    {
        private readonly IPaletteSettingsHolder palette;
        private readonly IFontSettingsHolder fontSettings;
        private readonly IImageSizeSettingsHolder sizeSettings;

        public CloudPainter(IPaletteSettingsHolder palette, IFontSettingsHolder fontSettings, 
            IImageSizeSettingsHolder sizeSettings)
        {
            this.palette = palette;
            this.fontSettings = fontSettings;
            this.sizeSettings = sizeSettings;
        }

        public void Paint(IEnumerable<Tag> cloud, Graphics g)
        {
            using var backgroundBush = new SolidBrush(palette.BackgroundColor);
            g.FillRectangle(backgroundBush, 0, 0, sizeSettings.Width, sizeSettings.Height);
            using var textBrush = new SolidBrush(palette.TextColor);
            foreach (var tag in cloud)
            {
                var font = fontSettings.Font;
                g.DrawString(tag.Word, new Font(font.FontFamily,
                        (float) tag.FontSize, font.Style,
                        font.Unit, font.GdiCharSet),
                    textBrush, tag.Location);
            }
        }
    }
}