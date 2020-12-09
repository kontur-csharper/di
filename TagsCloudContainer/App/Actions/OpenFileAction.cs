using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    internal class OpenFileAction : IUiAction
    {
        private readonly InputSettings settings;

        public OpenFileAction(InputSettings settings)
        {
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Открыть...";
        public string Description => "Использовать файл как источник тегов";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(settings.InputFileName),
                Filter = "Текстовые файлы (*.txt;*.docx)|*.txt;*.docx"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                settings.InputFileName = dialog.FileName;
        }
    }
}