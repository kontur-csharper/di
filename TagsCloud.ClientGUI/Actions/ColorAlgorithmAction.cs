using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI.Actions
{
    public class ColorAlgorithmAction : IUiAction
    {
        private readonly ColorAlgorithm colorAlgorithm;
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        public ColorAlgorithmAction(IImageHolder imageHolder, ImageSettings imageSettings,
            ColorAlgorithm colorAlgorithm)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
            this.colorAlgorithm = colorAlgorithm;
        }

        public string Category => "Настройки";
        public string Name => "Алгоритм расцветки...";
        public string Description => "Изменить алгоритм расцветки";

        public void Perform()
        {
            SettingsForm.For(colorAlgorithm).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}