using Autofac;
using TagCloud.Drawers;
using TagCloud.ImageSavers;
using TagCloud.TextReaders;
using TagCloud.Layouters;
using TagCloud.Settings;
using TagCloud.WordsAnalyzer;
using TagCloud.WordsAnalyzer.WordNormalizer;
using TagCloud.UserInterfaces;
using TagCloud.WordsAnalyzer.WordFilters;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new CommandLineInterface(args);
            
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterInstance(cli.FileReaderSettings).As<FileReaderSettings>();
            containerBuilder.RegisterInstance(cli.LayouterSettings).As<CircularLayouterSettings>();
            containerBuilder.RegisterInstance(cli.DrawerSettings).As<DrawerSettings>();
            containerBuilder.RegisterInstance(cli.SaverSettings).As<SaverSettings>();
            containerBuilder.RegisterInstance(new BoringWordFilter(cli.BoringWords)).As<IWordFilter>();
            containerBuilder.RegisterInstance(new GramPartsFilter(cli.GramParts)).As<IWordFilter>();
            
            containerBuilder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            containerBuilder.RegisterType<WordsAnalyzer.WordsAnalyzer>().As<IWordsAnalyzer>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();
            containerBuilder.RegisterType<FileReader>().As<ITextReader>();
            containerBuilder.RegisterType<ImageSaver>().As<IImageSaver>();
            containerBuilder.RegisterType<TagDrawer>().As<ITagDrawer>();
            
            containerBuilder.RegisterType<TagCloud>().AsSelf();

            var container = containerBuilder.Build();
            
            var tagCloud = container.Resolve<TagCloud>();
            tagCloud.MakeTagCloud();
        }
    }
}