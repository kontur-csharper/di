using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageHolder imageHolder;

        public SaveImageAction(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public string Category => "Файл";
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                DefaultExt = "",
                FileName = "image.png",
                Filter = "(*.png)|*.png|(*.jpg)|*.jpg|(*.bmp)|*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }
    }
}