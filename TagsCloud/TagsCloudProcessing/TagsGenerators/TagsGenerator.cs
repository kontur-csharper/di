using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.Factory;
using TagsCloud.Layouter;
using TagsCloud.TextProcessing;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.TagsCloudProcessing.TagsGenerators
{
    public class TagsGenerator : ITagsGenerator
    {
        private readonly IServiceFactory<IRectanglesLayouter> layouterFactory;
        private readonly WordConfig wordsConfig;

        public TagsGenerator(IServiceFactory<IRectanglesLayouter> layouterFactory, WordConfig wordsConfig)
        {
            this.layouterFactory = layouterFactory;
            this.wordsConfig = wordsConfig;
        }

        public IEnumerable<Tag> CreateTags(IEnumerable<WordInfo> words)
        {
            var layouter = layouterFactory.Create();

            var count = 30;
            return words.OrderByDescending(info => info.Frequence)
                 .Take(count)
                 .Select((wordInfo, index) =>
                 {
                     var font = new Font(wordsConfig.Font.FontFamily, count - index / 2);
                     var size = TextRenderer.MeasureText(wordInfo.Word, font);
                     return new Tag(wordInfo.Word, layouter.PutNextRectangle(size), font);
                 });
        }
    }
}
