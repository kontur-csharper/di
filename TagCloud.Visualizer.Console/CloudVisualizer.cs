using Autofac;
using CommandLine;
using GemBox.Document;
using TagCloud.ImageSaver;
using TagCloud.TextFileParser;

namespace TagCloud.Visualizer.Console
{
    internal static class CloudVisualizer
    {
        private static readonly InputOptions InputOptions = new InputOptions();
        private static readonly ImageOptions ImageOptions = new ImageOptions();

        private static readonly CenterOptions CenterOptions =
            new CenterOptions(ImageOptions.ImageWidth / 2, ImageOptions.ImageHeight / 2);

        private static void Main(string[] args)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            var cloudLayouterService = new ContainerBuilder();
            cloudLayouterService.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter(
                new TypedParameter(typeof(CenterOptions), CenterOptions));
            cloudLayouterService.RegisterType<BitmapSaver>().As<IImageSaver>();
            cloudLayouterService.RegisterType<PngSaver>().As<IImageSaver>();
            cloudLayouterService.RegisterComposite<CompositeImageSaver, IImageSaver>();
            cloudLayouterService.RegisterType<TxtFileParser>().As<ITextFileParser>();
            cloudLayouterService.RegisterType<WordDocumentParser>().As<ITextFileParser>();
            cloudLayouterService.RegisterComposite<FileParser, ITextFileParser>();
            cloudLayouterService.RegisterType<ToLowerCaseProcessor>().As<IWordsHandler>();
            var cloudLayouterBuilder = cloudLayouterService.Build();
            do
            {
                args = System.Console.ReadLine()?.Split(' ');
                var exitCode = ExecuteCommandAndReturnExitCode(args, cloudLayouterBuilder);
                if (exitCode == 1)
                {
                    System.Console.WriteLine("Некорректная команда");
                }
            } while (args?[0] != "exit");
        }

        private static int ExecuteCommandAndReturnExitCode(string[] args, IContainer cloudLayouterBuilder)
        {
            var exitCode = Parser.Default.ParseArguments<PrintCommand, ImageOptions>(args)
                .MapResult(
                    (PrintCommand command) =>
                        PrintCommand.PrintCloudAndReturnExitCode(cloudLayouterBuilder.Resolve<ICloudLayouter>(),
                            TextReader.GetWords(InputOptions,
                                cloudLayouterBuilder.Resolve<ITextFileParser>(),
                                cloudLayouterBuilder.Resolve<IWordsHandler>()),
                            ImageOptions,
                            cloudLayouterBuilder.Resolve<IImageSaver>()),
                    (InputOptions opts) => InputOptions.ChangeInputOptionsAndReturnExitCode(opts),
                    (ImageOptions opts) =>
                    {
                        var localExitCode = ImageOptions.ChangeImageOptionsAndReturnExitCode(opts);
                        CenterOptions.X = ImageOptions.ImageWidth / 2;
                        CenterOptions.Y = ImageOptions.ImageHeight / 2;
                        return localExitCode;
                    },
                    errs => 1);
            return exitCode;
        }
    }
}