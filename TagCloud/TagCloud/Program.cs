using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using CommandLine;
using NHunspell;
using TagCloud.ConsoleAppHelper;
using TagCloud.Curves;
using TagCloud.WordsFilter;
using TagCloud.WordsProvider;

namespace TagCloud
{
    public class Program
    {
        private static IContainer BuildDependencies(
            int width,
            int height,
            Color[] colors,
            string input)
        {
            var builder = new ContainerBuilder();
            var center = new Point(width / 2, height / 2);
            const double density = 0.05;
            const int angelStep = 5;
            var dictionaryAff = Path.GetFullPath("../../../../dictionaries/en.aff");
            var dictionaryDic = Path.GetFullPath("../../../../dictionaries/en.dic");
            builder.RegisterType<ArchimedeanSpiral>().As<ICurve>()
                .WithParameter("center", center)
                .WithParameter("density", density)
                .WithParameter("angelStep", angelStep);
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloud>().SingleInstance();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>();


            var wordsFilePath = Path.GetFullPath(input);
            var wordsFileExtension = Path.GetExtension(wordsFilePath);
            var wordsProviderType = WordsProviderFinder.FindFileWordsProvider(wordsFileExtension);
            builder.RegisterType(wordsProviderType).As<IWordsProvider>()
                .WithParameter("filePath", wordsFilePath);
            var wordsFilter = new WordsFilter.WordsFilter(new Hunspell(dictionaryAff, dictionaryDic))
                .Normalize()
                .RemovePrepositions();
            builder.RegisterInstance(wordsFilter).As<IWordsFilter>();

            builder.RegisterInstance(colors).As<Color[]>();
            return builder.Build();
        }

        private static IContainer BuildDependencies(Options options)
        {
            return BuildDependencies(options.Width, options.Height,
                ColorsParser.ParseColors(options.Colors),
                options.Input);
        }

        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            Options options;
            try
            {
                options = ((Parsed<Options>) result).Value;
            }
            catch
            {
                var errors = ((NotParsed<Options>) result).Errors;
                foreach (var error in errors) Console.WriteLine(error);
                return;
            }

            var container = BuildDependencies(options);

            container.Resolve<ITagCloud>().GenerateTagCloud();

            var bitmap = container.Resolve<IVisualizer>()
                .CreateBitMap(options.Width, options.Height, container.Resolve<Color[]>(), options.FontFamily);
            bitmap.Save(Path.GetFullPath(options.Output), ImageFormat.Png);
        }
    }
}