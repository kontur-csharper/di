using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagsCloud.BitmapCreator;
using TagsCloud.ColoringAlgorithms;
using TagsCloud.FileReaders;
using TagsCloud.ImageConfig;
using TagsCloud.LayoutAlgorithms;
using TagsCloud.PointGenerator;
using TagsCloud.WordFilters;

namespace TagsCloud
{
    public static class Program
    {
        private static string _pathToInputFile;
        private static Color _backgroundColor;
        private static Color _textColor;
        private static FontFamily _fontFamily;
        private static string _format;
        private static Size _size;
        private static IServiceProvider serviceProvider;
        private static Action _helpFunc;

        public static int Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.ShowHelp(true);
            ConfigureCommandLineApp(app);
            try
            {
                app.Execute(args);
            }
            catch (Exception)
            {
                Console.WriteLine("Введенная вами команда некоректна. Для подробностей --help");
                Process.GetCurrentProcess().Kill();
            }

            ConfigureServices();

            var text = serviceProvider.GetService<IFileReader>()?.GetWordsFromFile(_pathToInputFile);
            var words = serviceProvider.GetService<IWordFilter>()?.FilterWords(text).Select(x => x.ToLower()).ToArray();
            var bitmapCreator = serviceProvider.GetService<IBitmapCreator>();
            var image = bitmapCreator.Create(words);
            BitmapSaver.BitmapSaver.Save(image, _format);
            bitmapCreator.Dispose();
            return 0;
        }

        private static void ConfigureCommandLineApp(CommandLineApplication app)
        {
            var help = app.Option("-h|--help <HELP>", "Help", CommandOptionType.NoValue);
            var input = app.Option("-i|--input <INPUT>", "Input file path", CommandOptionType.SingleValue);
            var backgroundColor = app.Option<Color>("-b|--bgcolor <BGCOLOR>", "Background color",
                CommandOptionType.SingleValue);
            var textColor = app.Option<Color>("-t|--tagcolor <TAGCOLOR>", "Tags color", CommandOptionType.SingleValue);
            var font = app.Option("-f|--font <FONT>", "Font family", CommandOptionType.SingleValue);
            var format = app.Option("-r|--format <FORMAT>", "Result image format", CommandOptionType.SingleValue);
            var size = app.Option("-s|--size <SIZE>", "Result image size", CommandOptionType.SingleValue);

            app.OnExecute(() =>
                {
                    if (help.HasValue())
                    {
                        foreach (var i in app.Options)
                            Console.WriteLine($"--{i.LongName}, -{i.ShortName} is {i.Description}");
                        Process.GetCurrentProcess().Kill();
                    }

                    if (!input.HasValue())
                    {
                        Console.WriteLine("ArgumentException: не указан путь к файлу. Для подробностей --help");
                        Process.GetCurrentProcess().Kill();
                    }

                    _pathToInputFile = ArgumentParser.CheckFilePath(input.Value());

                    _backgroundColor = backgroundColor.HasValue()
                        ? ArgumentParser.GetColor(backgroundColor.Value())
                        : Color.Bisque;

                    _textColor = textColor.HasValue()
                        ? ArgumentParser.GetColor(textColor.Value())
                        : Color.Aquamarine;

                    _fontFamily = font.HasValue()
                        ? ArgumentParser.GetFontFamily(font.Value())
                        : new FontFamily("Arial");

                    _format = format.HasValue() && ArgumentParser.IsCorrectFormat(format.Value())
                        ? format.Value()
                        : "png";

                    _size = size.HasValue()
                        ? ArgumentParser.GetSize(size.Value())
                        : new Size(1200, 1200);

                    return 0;
                }
            );
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IWordFilter>(new PartsOfSpeechFilter(PartsOfSpeech.ADVPRO, PartsOfSpeech.APRO,
                PartsOfSpeech.CONJ));
            var coloringAlgorithm = new DefaultColoringAlgorithm(_textColor);
            services.AddSingleton<ILayoutAlgorithm>(
                new CircularCloudLayouter(new Point(_size.Width / 2, _size.Height / 2)));
            services.AddSingleton<IColoringAlgorithm>(coloringAlgorithm);
            services.AddSingleton<IImageConfig>(new ImageConfig.ImageConfig(_size, _fontFamily, _backgroundColor,
                coloringAlgorithm));
            services.AddSingleton<IPointGenerator, ArchimedeanSpiral>();
            services.AddSingleton(ChooseReader());
            services.AddSingleton<IBitmapCreator, BitmapCreator.BitmapCreator>();
            serviceProvider = services.BuildServiceProvider();
        }

        private static IFileReader ChooseReader()
        {
            if (_pathToInputFile.EndsWith(".txt"))
                return new TxtFileReader();

            if (_pathToInputFile.EndsWith(".docx"))
                return new DocxFileReader();

            throw new ArgumentException("Unsupported file format");
        }
    }
}

