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
    public class ExampleAction : IFormAction
    {
        public string Category => "Tag cloud";
        public string Name => "Build example";
        public string Description => "Build example tag cloud";

        private readonly ITagCloudBuilder tagCloudBuilder;
        private readonly ICloudVisualizer cloudVisualizer;
        private readonly ITextReader textReader;
        private readonly ITextHandler textHandler;
        private readonly SpiralParams spiralParams;

        public ExampleAction(
            ITagCloudBuilder tagCloudBuilder, ICloudVisualizer cloudVisualizer, 
            ITextReader textReader, ITextHandler textHandler, SpiralParams spiralParams)
        {
            this.tagCloudBuilder = tagCloudBuilder;
            this.cloudVisualizer = cloudVisualizer;
            this.textReader = textReader;
            this.textHandler = textHandler;
            this.spiralParams = spiralParams;
        }

        public void Perform()
        {
            var result = SettingsForm.For(spiralParams).ShowDialog();
            if (result != DialogResult.OK)
                return;

            var text = textReader.ReadAllText(@"..\..\..\Examples\example.txt");
            var handledWords = textHandler.GetHandledWords(text);
            var tagCloud = tagCloudBuilder.Build(handledWords);
            cloudVisualizer.PrintTagCloud(tagCloud);
        }
    }
}