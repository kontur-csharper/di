using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Spirals;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI.Actions
{
    public class SpiralSettingAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;
        private readonly SpiralSettings spiralSettings;

        public SpiralSettingAction(IImageHolder imageHolder, ImageSettings imageSettings, SpiralSettings spiralSettings)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
            this.spiralSettings = spiralSettings;
        }

        public string Category => "Настройки";
        public string Name => "Коэффициент спирали...";
        public string Description => "Изменить коэффициент спирали";

        public void Perform()
        {
            SettingsForm.For(spiralSettings).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}