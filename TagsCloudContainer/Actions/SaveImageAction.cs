using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.Common;
using TagsCloudContainer.UiActions;

namespace TagsCloudContainer.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly FilesSettings settings;

        public SaveImageAction(FilesSettings filesSettings, IImageHolder imageHolder)
        {
            settings = filesSettings;
            this.imageHolder = imageHolder;
        }

        public string Category => "Файлы";
        public string Name => "Сохранить облако тегов...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(settings.DirectoryToSavePictures),
                DefaultExt = "png",
                FileName = settings.PictureFileName,
                Filter = "Image Files(*.BMP;*.JPEG;*.JPG;*.PNG;)|*.BMP;*.JPEG;*.JPG;*.PNG|All files (*.*)|*.*"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }
    }
}