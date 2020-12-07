using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI.Actions
{
    public class FontSettingsAction : IUiAction
    {
        private readonly FontSetting font;
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        public FontSettingsAction(IImageHolder imageHolder, ImageSettings imageSettings, FontSetting font)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
            this.font = font;
        }

        public string Category => "Настройки";
        public string Name => "Шрифт...";
        public string Description => "Изменить шрифт";

        public void Perform()
        {
            SettingsForm.For(font).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}