using TagsCloudContainer.Common;
using TagsCloudContainer.UiActions;

namespace TagsCloudContainer.Actions
{
    public class FontSettingsAction : IUiAction

    {
        private readonly FontSettings fontSettings;

        public FontSettingsAction(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
        }

        public string Category => "Размеры";
        public string Name => "Шрифт...";
        public string Description => "Размеры шрифта тегов";

        public void Perform()
        {
            SettingsForm.For(fontSettings).ShowDialog();
        }
    }
}