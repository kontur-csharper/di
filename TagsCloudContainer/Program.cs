using Autofac;
using CommandLine;
using TagsCloudContainer.Drawer;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.ProgramOptions;
using TagsCloudContainer.WordsParser;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
            var builder = new ContainerBuilder();
            
            builder.RegisterType<ImageOptions>().As<IImageOptions>()
                .WithParameters(new []
                {
                    new NamedParameter("width", options.Width), 
                    new NamedParameter("height", options.Height),
                    new NamedParameter("imageOutputDirectory", options.OutputDirectory),
                    new NamedParameter("imageName", options.OutputFileName),
                    new NamedParameter("imageExtension", options.OutputFileExtension)
                });
            builder.RegisterType<FontOptions>().As<IFontOptions>()
                .WithParameters(new []
                {
                    new NamedParameter("fontFamily", options.FontFamily), 
                    new NamedParameter("fontColor", options.FontColor) 
                });
            builder.RegisterType<FilterOptions>().As<IFilterOptions>()
                .WithParameters(new []
                {
                    new NamedParameter("mystemLocation", options.MystemLocation), 
                    new NamedParameter("boringWords", options.BoringWords) 
                });
            builder.RegisterType<WordsAnalyzer>().As<IWordsAnalyzer>();
            builder.RegisterType<FileReader>().As<IWordReader>().WithParameter("filePath", options.FilePath);
            builder.RegisterType<Filter>().As<IFilter>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<LayoutDrawer>().As<ILayoutDrawer>();
            builder.RegisterType<RectangleLayout>().As<IRectangleLayout>();
            builder.RegisterType<TagCloudContainer>().As<ITagCloudContainer>();
            var container = builder.Build();

            var tagCloudContainer = container.Resolve<ITagCloudContainer>();
            tagCloudContainer.MakeTagCloud();
            tagCloudContainer.SaveTagCloud();
        }
    }
}