using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloud.UI
{
    public partial class Mainform : Form
    {
        private readonly HashSet<IFileReader> fileReaders;
        private readonly IWordsFilter filter;
        private readonly Dictionary<string, FontFamily> fontFamilies;
        private readonly Dictionary<string, FontStyle> fontStyles;
        private readonly Dictionary<string, IRectanglesLayouter> layouters;
        private readonly TagsCloudSettings settings;
        private readonly ITagsCloudDrawer tagsCloudDrawer;
        private readonly TagsCloudHandler tagsCloudHandler;

        public Mainform(IRectanglesLayouter[] rectanglesLayouters,
            TagsCloudHandler tagsCloudHandler,
            ITagsCloudDrawer drawer,
            TagsCloudSettings settings,
            IWordsFilter filter,
            IEnumerable<IFileReader> fileReaders)
        {
            this.tagsCloudHandler = tagsCloudHandler;
            this.settings = settings;
            this.filter = filter;
            this.fileReaders = fileReaders.ToHashSet();
            tagsCloudDrawer = drawer;
            fontFamilies = settings.FontSettings.FontFamilies.ToDictionary(family => family.Name);
            fontStyles = settings.FontSettings.FontStyles.ToDictionary(style => style.ToString());
            layouters = rectanglesLayouters.ToDictionary(c => c.Name);
            InitializeComponent();
            FontFamilyChoice.DataSource = settings.FontSettings.FontFamilies.Select(f => f.Name).ToList();
            FontStyleChoice.DataSource = settings.FontSettings.FontStyles.Select(f => f.ToString()).ToList();
            AlgorithmChoice.DataSource = rectanglesLayouters.Select(c => c.Name).ToList();
        }

        private void SetPaletteButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<Palette>(settings.Palette).ShowDialog();
        }

        private void SetImageSizeButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<ImageSize>(settings.ImageSize).ShowDialog();
        }

        private void ImageSaveButton_Click(object sender, EventArgs e)
        {
            if (PictureBox.Image != null && SaveFileDialog.ShowDialog() == DialogResult.OK)
                PictureBox.Image.Save(SaveFileDialog.FileName);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = OpenFileDialog.FileName;
                var fileType = fileName.Split('.')[^1];
                var fileReader = fileReaders.First(reader => reader.AvailableFileTypes.Contains(fileType));
                if (fileReader == null)
                    throw new InvalidOperationException("Invalid file");
                PictureBox.Image = tagsCloudHandler.GetNewTagcloud(fileReader.ReadWords(fileName));
            }
        }

        private void FontFamilyChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.CurrentFontFamily = fontFamilies[(string) FontFamilyChoice.SelectedItem];
        }

        private void FontStyleChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.CurrentFontStyle = fontStyles[(string) FontStyleChoice.SelectedItem];
        }

        private void AlgorithmChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            tagsCloudDrawer.SetNewLayouter(layouters[(string) AlgorithmChoice.SelectedItem]);
        }

        private void CloudSizeSetting_ValueChanged(object sender, EventArgs e)
        {
            settings.CloudToImageScaleRatio = (float) CloudSizeSetting.Value;
        }

        private void ExcludedWordsSetButton_Click(object sender, EventArgs e)
        {
            if (filter is BlackListWordsFilter blackListfilter) new SetExcludingWordsForm(blackListfilter).ShowDialog();
        }

        private void MaxWordsCountSetting_ValueChanged(object sender, EventArgs e)
        {
            settings.MaxWordsCount = (int) MaxWordsCountSetting.Value;
        }
    }
}