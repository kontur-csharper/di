using TagsCloudContainer.Common;
using TagsCloudContainer.UiActions;

namespace TagsCloudContainer.Actions
{
    internal class FilesSettingsAction : IUiAction
    {
        private readonly FilesSettings filesSettings;

        public FilesSettingsAction(FilesSettings filesSettings)
        {
            this.filesSettings = filesSettings;
        }

        public string Category => "Файлы";
        public string Name => "Данные...";
        public string Description => "Файлы с данными для облака тегов";

        public void Perform()
        {
            SettingsForm.For(filesSettings).ShowDialog();
        }
    }
}