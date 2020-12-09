using System.Drawing;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class FontGetter : IFontGetter
    {
        private readonly IFontSettingsHolder settings;

        public FontGetter(IFontSettingsHolder settings)
        {
            this.settings = settings;
        }

        public Font GetFont(string word, double frequency)
        {
            var font = settings.Font;
            var fontSize = font.Size * (1 + frequency * font.Size);
            return new Font(font.FontFamily,
                (float) fontSize, font.Style,
                font.Unit, font.GdiCharSet);
        }
    }
}