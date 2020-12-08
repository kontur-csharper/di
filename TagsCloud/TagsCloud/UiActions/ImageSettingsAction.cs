using TagsCloud.GUI;
using TagsCloud.Infrastructure;

namespace TagsCloud.UiActions
{
    public class ImageSettingsAction : IUiAction
    {
        private ImageSettings settings;
        private IImageHolder holder;
        public ImageSettingsAction(ImageSettings settings, IImageHolder holder)
        {
            this.settings = settings;
            this.holder = holder;
        }

        public string Category => "Настройки";
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            var dialog = new SettingsForm(holder);
            dialog.ShowDialog();
        }
    }
}