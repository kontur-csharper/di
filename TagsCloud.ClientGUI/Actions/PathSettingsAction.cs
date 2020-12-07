using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI.Actions
{
    public class PathSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;
        private readonly PathSettings pathSettings;

        public PathSettingsAction(IImageHolder imageHolder, ImageSettings imageSettings, PathSettings pathSettings)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
            this.pathSettings = pathSettings;
        }

        public string Category => "Настройки";
        public string Name => "Расположение текстов...";
        public string Description => "Изменить расположение текстов";

        public void Perform()
        {
            SettingsForm.For(pathSettings).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}