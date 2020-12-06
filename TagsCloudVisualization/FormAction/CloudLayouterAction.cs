using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.PointsGenerators;
using TagsCloudVisualization.TagCloudBuilders;
using TagsCloudVisualization.TagCloudVisualizers;
using TagsCloudVisualization.TextProcessing.TextHandler;
using TagsCloudVisualization.TextProcessing.TextReader;

namespace TagsCloudVisualization.FormAction
{
    public class CloudLayouterAction : IFormAction
    {
        public string Category => "Tag cloud";
        public string Name => "Build tag cloud";
        public string Description => "Build your tag cloud from text";

        private readonly ITagCloudBuilder tagCloudBuilder;
        private readonly ICloudVisualizer cloudVisualizer;
        private readonly ITextReader textReader;
        private readonly ITextHandler textHandler;
        private readonly SpiralParams spiralParams;

        public CloudLayouterAction(
            ITagCloudBuilder tagCloudBuilder, ICloudVisualizer cloudVisualizer, ITextReader textReader,
            ITextHandler textHandler, SpiralParams spiralParams)
        {
            this.tagCloudBuilder = tagCloudBuilder;
            this.cloudVisualizer = cloudVisualizer;
            this.textReader = textReader;
            this.textHandler = textHandler;
            this.spiralParams = spiralParams;
        }

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select file to build a tag cloud",
                CheckFileExists = false,
                Multiselect = false,
                DefaultExt = "txt",
                InitialDirectory = @"C:\Users\Public\Documents",
                Filter = "Текстовый документ |*.txt;*.doc;*.docx"
            };
            var res = dialog.ShowDialog();
            if (res != DialogResult.OK)
                return;

            SettingsForm.For(spiralParams).ShowDialog();

            var text = textReader.ReadAllText(dialog.FileName);
            var handledWords = textHandler.GetHandledWords(text);
            var tagCloud = tagCloudBuilder.Build(handledWords);
            cloudVisualizer.PrintTagCloud(tagCloud);
        }
    }
}