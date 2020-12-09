using System.Drawing;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.Settings
{
    public class FontSettings : IFontSettingsHolder
    {
        public static readonly FontSettings Instance = new FontSettings();

        private FontSettings()
        {
            Font = new Font("Arial", 10);
        }

        public Font Font { get; set; }
    }
}