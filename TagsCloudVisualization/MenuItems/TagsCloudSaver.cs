using System;
using System.Windows.Forms;
using TagsCloudVisualization.App;
using TagsCloudVisualization.Contracts;

namespace TagsCloudVisualization.MenuItems
{
    public class TagsCloudSaver : Form, IMenuItem
    {
        public string MenuAffiliation => "File";
        public string ItemName => "Save...";
        private TagsCloudPictureHolder TagsCloudPictureHolder { get; }

        public TagsCloudSaver(TagsCloudPictureHolder tagsCloudPictureHolder) =>
            TagsCloudPictureHolder = tagsCloudPictureHolder;

        public DialogResult Execute() => SaveInFile();

        private DialogResult SaveInFile()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
                DefaultExt = "png",
                FileName = "TagsCloud.png",
                Filter = "Images (*.png)|*.png"
            };

            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
                TagsCloudPictureHolder.SaveImage(dialog.FileName);
            return dialogResult;
        }
    }
}