using System;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.DataReader;
using TagsCloudContainer.Infrastructure.TextAnalyzer;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudVisualizer : ICloudVisualizer
    {
        private readonly ICloudGenerator cloudGenerator;
        private readonly Lazy<IImageHolder> imageHolder;
        private readonly IDataReaderFactory inputDataReaderFactory;
        private readonly ICloudPainter painter;
        private readonly ITextAnalyzer textParserToFrequencyDictionary;

        public CloudVisualizer(IDataReaderFactory inputDataReaderFactory,
            ITextAnalyzer textParserToFrequencyDictionary,
            ICloudGenerator cloudGenerator, ICloudPainter painter, Lazy<IImageHolder> imageHolder)
        {
            this.inputDataReaderFactory = inputDataReaderFactory;
            this.textParserToFrequencyDictionary = textParserToFrequencyDictionary;
            this.cloudGenerator = cloudGenerator;
            this.painter = painter;
            this.imageHolder = imageHolder;
        }

        public void Visualize()
        {
            var lines = inputDataReaderFactory.CreateDataReader().ReadLines();
            var frequencyDictionary = textParserToFrequencyDictionary.GenerateFrequencyDictionary(lines);
            var cloud = cloudGenerator.GenerateCloud(frequencyDictionary);
            painter.Paint(cloud, imageHolder.Value.StartDrawing());
            imageHolder.Value.UpdateUi();
        }
    }
}