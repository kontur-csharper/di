using System.Drawing;
using System.IO;
using TagsCloudVisualization;
using TagsCloudVisualization.Configs;
using TagsCloudVisualization.WordsCleaners;
using TagsCloudVisualization.WordsConverters;
using TagsCloudVisualization.WordsProviders;

namespace CloudContainer
{
    public class TagCloudContainer
    {
        private readonly IWordsCleaner cleaner;
        private readonly IConfig config;
        private readonly IWordConverter converter;
        private readonly IWordProvider provider;


        public TagCloudContainer(IWordsCleaner cleaner, IWordConverter converter,
            IWordProvider provider, IConfig config)
        {
            this.cleaner = cleaner;
            this.converter = converter;
            this.provider = provider;
            this.config = config;
        }


        public Bitmap GetImage(TagCloudArguments arguments)
        {
            config.SetValues(arguments.Font, arguments.Center,
                arguments.TextColor, arguments.ImageSize, arguments.BoringWords);
            cleaner.AddBoringWords(config.BoringWords);

            var path = Path.Join(Directory.GetCurrentDirectory(), arguments.InputFileName);

            var words = provider.GetWords(path);
            var cleanedWords = cleaner.CleanWords(words);

            var cloudTags = converter.ConvertWords(cleanedWords);

            return Drawer.DrawImage(cloudTags, config);
        }
    }
}