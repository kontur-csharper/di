using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;
        private readonly Palette palette;

        public PaletteSettingsAction(IImageHolder imageHolder, ImageSettings imageSettings, Palette palette)
        {
            this.palette = palette;
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public string Category => "Настройки";
        public string Name => "Палитра...";
        public string Description => "Цвета для рисования облака";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}