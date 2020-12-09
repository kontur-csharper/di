using System.Drawing;

namespace TagsCloudContainer.Infrastructure.Settings
{
    interface IPaletteSettingsHolder
    {
        public Color TextColor { get; }
        public Color BackgroundColor { get; }
    }
}
