using System.Drawing;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.Settings
{
    public class Palette : IPaletteSettingsHolder
    {
        public static readonly Palette Instance = new Palette();

        private Palette()
        {
            TextColor = Color.White;
            BackgroundColor = Color.Black;
        }

        public Color TextColor { get; set; }
        public Color BackgroundColor { get; set; }
    }
}