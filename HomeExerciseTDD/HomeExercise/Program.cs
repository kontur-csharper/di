using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace HomeExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordPath = @"word3.txt";
            
            var boringPath = @"boringWord.txt";
            args = new[] {"options", "--words", wordPath, "--boring", boringPath, "--format", "bmp", "--color", "110", "-c", "24", "--imageName", "any"};
            //args = new[] {"options", "--words", wordPath};
            
            
            var console = BuildConsole();
            var builder = new ContainerBuilder();
            console.HandleSettingsFromConsole(args, builder);
            var painter = BuildPainter(builder);

            painter.DrawFigures();
        }

        private static IConsoleCloudClient BuildConsole()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleCloudClient>().As<IConsoleCloudClient>();
            var consoleContainer = builder.Build();
            return consoleContainer.Resolve<IConsoleCloudClient>();
        }
        
        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.Func`2[System.Drawing.Rectangle,System.Boolean]")]
        private static IPainter BuildPainter(ContainerBuilder builder)
        {
            builder.RegisterType<WordsProcessor>().As<IWordsProcessor>();
            builder.RegisterType<Spiral>().As<ISpiral>();
            builder.RegisterType<Word>().As<IWord>();
            builder.RegisterType<WordCloud>().As<IWordCloud>();
            builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>();
            builder.RegisterType<WordCloudPainter>().As<IPainter>();
            var container = builder.Build();
            return container.Resolve<IPainter>();
        }
    }
}