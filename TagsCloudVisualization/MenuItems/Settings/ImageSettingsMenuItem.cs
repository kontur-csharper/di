using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.App;
using TagsCloudVisualization.Contracts;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Common;

namespace TagsCloudVisualization.MenuItems.Settings
{
    public class ImageSettingsMenuItem : IMenuItem
    {
        public string MenuAffiliation => "Settings";
        public string ItemName => "Image...";
        private ImageSettings Settings { get; }
        private TagsCloudPictureHolder ImageHolder { get; }

        public ImageSettingsMenuItem(ImageSettings settings, TagsCloudPictureHolder imageHolder)
        {
            Settings = settings;
            ImageHolder = imageHolder;
        }

        public DialogResult Execute()
        {
            var dialogResult = SettingsForm.For(Settings).ShowDialog();
            var mainForm = Application.OpenForms[nameof(MainForm)];

            if (dialogResult is not DialogResult.OK) 
                return dialogResult;
            
            mainForm!.ClientSize = new Size(Settings.Width, Settings.Height);
            ImageHolder.RecreateImage();

            return dialogResult;
        }
    }
}