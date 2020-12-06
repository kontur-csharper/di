using System;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class AppProcessor : IAppProcessor
    {
        private readonly IVisualization _visualization;
        private readonly IWordTagsLayouter _wordTagsLayouter;
        private readonly IFileReader _fileReader;
        private readonly IImageSaver _imageSaver;

        public AppProcessor(IVisualization visualization,
            IWordTagsLayouter wordTagsLayouter,
            IFileReader fileReader,
            IImageSaver imageSaver)
        {
            _visualization = visualization;
            _wordTagsLayouter = wordTagsLayouter;
            _fileReader = fileReader;
            _imageSaver = imageSaver;
        }

        public void Run()
        {
            var originalText = _fileReader.GetTextFromFile();
            var (tags, cloudRadius) = _wordTagsLayouter.GetWordTagsAndCloudRadius(originalText);
            if (tags.Count == 0)
            {
                Console.WriteLine("No interesting words for drawing");
                return;
            }

            using var cloudImage = _visualization
                .GetImageCloud(tags, cloudRadius);
            _imageSaver.Save(cloudImage);
        }
    }
}