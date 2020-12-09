using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly OutputSettings outputSettings;
        private readonly IImageHolder imageHolder;

        public SaveImageAction(IImageHolder imageHolder, OutputSettings outputSettings)
        {
            this.imageHolder = imageHolder;
            this.outputSettings = outputSettings;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(outputSettings.OutputFilePath),
                Filter = "Изображения (*.png;*.jpeg;*.bmp)|*.png;*.jpeg;*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                outputSettings.OutputFilePath = dialog.FileName;
                imageHolder.SaveImage();
            }
        }
    }
}