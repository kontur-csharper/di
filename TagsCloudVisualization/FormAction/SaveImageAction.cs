using System.Windows.Forms;
using TagsCloudVisualization.Canvases;
using TagsCloudVisualization.ImageSavers;

namespace TagsCloudVisualization.FormAction
{
    public class SaveImageAction : IFormAction
    {
        public string Category => "File";
        public string Name => "Save as image";
        public string Description => "Save your tag cloud to a folder";

        private readonly ICanvas canvas;
        private readonly IImageSaver imageSaver;

        public SaveImageAction(ICanvas canvas, IImageSaver imageSaver)
        {
            this.canvas = canvas;
            this.imageSaver = imageSaver;
        }

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = @"C:\Users\Public\Pictures",
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Images |*.bmp|Images |*.jpg;*.jpeg|Images |*.png|Images |*.gif"
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
                imageSaver.SaveImage(canvas, dialog.FileName);
        }
    }
}