using System;
using System.Drawing;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.BackgroundPainter;
using TagCloud.Layout;
using TagCloud.TextProcessing;

namespace TagCloud
{
    static class Program
    {
        private static IServiceProvider serviceProvider;
        private static CommandLineApplication app = new CommandLineApplication();
        
        public static int Main(string[] args)
        {
            var CLI = new CommandLineInterface();
            CLI.ConfigureCLI(app);
            var executionResult = app.Execute(args);
            if (executionResult == 0)
            {
                return 0;
            }
            ConfigureServices(CLI.CanvasSize, CLI.BackgroundType, CLI.FileName);
            
            var visualizer = serviceProvider.GetService<IVisualizer>();
            var pathToPng = visualizer.Visualize(CLI.FileName, CLI.StringFont, CLI.StringColor);
            
            Console.WriteLine("Result saved to:\n" + pathToPng);

            return 0;
        }
        
        private static void ConfigureServices(Size size, Background background, string filename)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPathCreater, PathCreater>();
            ConfigureTextReader(services, filename);
            services.AddSingleton<IWordParser, LiteratureTextParser>();
            services.AddSingleton<IFrequencyAnalyzer, FrequencyAnalyzer>();
            services.AddSingleton<ICanvas>(_ => new Canvas(size.Width, size.Height));
            services.AddSingleton<ISpiral, Spiral>();
            services.AddSingleton<ILayouter, Layouter>();
            services.AddSingleton<ITagsCreater, TagsCreater>();
            ConfigureBackgroundPainterService(services, background);
            services.AddSingleton<IVisualizer, Visualizer>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureTextReader(ServiceCollection services, string filename)
        {
            if (filename.EndsWith(".txt"))
                services.AddSingleton<ITextReader, TxtTextReader>();
            else if (filename.EndsWith(".docx"))
                services.AddSingleton<ITextReader, DocxTextReader>();
            else
                throw new ArgumentException("Unhandled input format");
        }

        private static void ConfigureBackgroundPainterService(ServiceCollection services, Background background)
        {
            switch (background)
            {
                case Background.Circle:
                    services.AddSingleton<IBackgroundPainter, BackgroundPainterCircle>();
                    break;
                case Background.Empty:
                    services.AddSingleton<IBackgroundPainter, BackgroundPainterEmpty>();
                    break;
                case Background.Rectangles:
                    services.AddSingleton<IBackgroundPainter, BackgroundPainterRectangles>();
                    break;
                default:
                    throw new ArgumentException("Unhandled background type " + background);
            }
        }
    }
}