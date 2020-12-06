using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.FormAction
{
    public class FontAction : IFormAction
    {
        public string Category => "Settings";
        public string Name => "Fonts";
        public string Description => "Change fonts of your tag cloud visualization";

        private readonly FontSettings fontSettings;

        public FontAction(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
        }

        public void Perform()
        {
            SettingsForm.For(fontSettings).ShowDialog();
        }
    }
}