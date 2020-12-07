using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagCloud.Commands;
using TagCloud.Controllers;
using TagCloud.Extensions;
using TagCloud.Layouters;
using TagCloud.Renderers;
using TagCloud.Settings;
using TagCloud.Sources;
using TagCloud.TagClouds;
using TagCloud.Visualizers;

namespace TagCloud
{
    public class Program
    {
        private static readonly string[] defaultBoringWords = {"а", "от", "без", "что", "как", "но"};

        private static IContainer container;

        public static void Main(string[] args)
        {
            BuildContainer();
            Start();
        }

        private static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => new SourceSettings
            {
                Destination = "data/example.txt",
                Ignore = new List<string>(defaultBoringWords)
            }).SingleInstance();
            builder.Register(ctx => new ResultSettings
            {
                OutputPath = "created/"
            }).SingleInstance();
            builder.Register(ctx => CloudSettings.GetDefault()).SingleInstance();

            builder.RegisterType<TextSource>().As<ISource>();
            builder.RegisterType<DocxSource>().As<ISource>();

            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();

            builder.RegisterType<CircleTagCloud>().AsSelf().As<TagCloud<Rectangle>>();

            builder.RegisterType<DistanceColorVisualizer>().AsSelf().As<IVisualizer<RectangleTagCloud>>();

            builder.RegisterType<FileCloudRender>().As<IRender>();

            builder.RegisterCommand<HelpCommand>();
            builder.RegisterCommand<CreateImageCommand>();
            builder.RegisterCommand<SourceCommand>();
            builder.RegisterCommand<CloudSettingsCommand>();
            builder.RegisterCommand<IgnoreCommand>();

#if DEBUG
            builder.RegisterCommand<DebugCommand>();
#endif

            builder.RegisterType<ConsoleController>().As<IController>();

            container = builder.Build();
        }

        private static void Start()
        {
            using var scope = container.BeginLifetimeScope();
            var controller = scope.Resolve<IController>();
            controller.Start();
        }
    }
}
