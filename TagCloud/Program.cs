using System.Drawing;
using System.Linq;
using Autofac;
using McMaster.Extensions.CommandLineUtils;
using TagCloud.Factory;
using TagCloud.Interfaces;
using TagsCloudVisualization;

namespace TagCloud
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WordsNormalizer>().As<IWordsNormalizer>();
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>();
            builder.RegisterType<SpiralPointsFactory>().As<IPointsFactory>();
            builder.RegisterType<ColorGeneratorFactory>().As<IColorGeneratorFactory>();
            builder.RegisterType<CloudDrawerFactory>().As<ICloudDrawerFactory>();
            builder.RegisterType<WordsReader>().As<IWordsReader>();
            builder.RegisterType<TagCloudLayouterFactory>().As<ITagCloudLayouterFactory>();
            builder.RegisterType<WordsForCloudGeneratorFactory>().As<IWordsForCloudGeneratorFactory>();
            builder.RegisterType<TagCloudCreatorFactory>().As<ITagCloudCreatorFactory>();
            builder.RegisterType<ConsoleInterface>().As<IProgramInterface>();

            var programInterface = builder.Build().Resolve<IProgramInterface>();

            programInterface.Run(args);
        }
    }
}