using System;
using System.Drawing;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class ConsoleInterface : IProgramInterface
    {
        private ITagCloudCreatorFactory tagCloudCreatorFactory;

        public ConsoleInterface(ITagCloudCreatorFactory tagCloudCreatorFactory)
        {
            this.tagCloudCreatorFactory = tagCloudCreatorFactory;
        }

        public void Run(string[] args)
        {
            var tagCloudSettings = ParseArguments(args);
            CreateTagCloud(tagCloudCreatorFactory, tagCloudSettings);
        }

        private static TagCloudSettings ParseArguments(string[] args)
        {
            var app = new CommandLineApplication();
            app.HelpOption();

            var pictureSizeArg = app.Option<int>("-s|--pictureSize <int,int>", "Picture size",
                                                 CommandOptionType.MultipleValue);
            var cloudCenterArg = app.Option<int>("-c|--cloudCenter <int,int>", "Cloud center",
                                                 CommandOptionType.MultipleValue);
            var colorsArgs =
                app.Option<string>("-o|--colors <name,name,...>", "Colors", CommandOptionType.MultipleValue);
            var fontNameArg = app.Option<string>("-f|--font <name>", "font", CommandOptionType.SingleValue);
            var maxFontSizeArg =
                app.Option<int>("-m|--maxFontSize <int>", "Max font size", CommandOptionType.SingleValue);
            var inputFileArg =
                app.Option<string>("-n|--inputFile <path>", "Input file .txt", CommandOptionType.SingleValue);
            var outputFileArg =
                app.Option<string>("-u|--outputFile <path>", "output file", CommandOptionType.SingleValue);
            var boringWordsFileArg = app.Option<string>("-b|--boringWords <path>", "boring words file",
                                                        CommandOptionType.SingleValue);

            TagCloudSettings tagCloudSettings = null;

            app.OnExecute(() =>
            {
                var pictureSize = pictureSizeArg.HasValue() && pictureSizeArg.Values.Count == 2
                    ? new Size(pictureSizeArg.ParsedValues[0], pictureSizeArg.ParsedValues[1])
                    : new Size(2000, 2000);

                var cloudCenter = cloudCenterArg.HasValue() && cloudCenterArg.Values.Count == 2
                    ? new Point(cloudCenterArg.ParsedValues[0], cloudCenterArg.ParsedValues[1])
                    : new Point(1000, 1000);

                var colors = colorsArgs.HasValue()
                    ? cloudCenterArg.Values.Select(Color.FromName).ToArray()
                    : new[] {Color.Black};

                var fontName = fontNameArg.HasValue()
                    ? fontNameArg.ParsedValue
                    : "Arial";

                var maxFontSize = maxFontSizeArg.HasValue()
                    ? maxFontSizeArg.ParsedValue
                    : 40;

                var inputFile = inputFileArg.HasValue()
                    ? inputFileArg.ParsedValue
                    : "in.txt";

                var outputFile = outputFileArg.HasValue()
                    ? outputFileArg.ParsedValue
                    : "out.png";

                var boringWordsFile = boringWordsFileArg.HasValue()
                    ? boringWordsFileArg.ParsedValue
                    : "boring.txt";

                tagCloudSettings = new TagCloudSettings(pictureSize,
                                                        cloudCenter,
                                                        colors,
                                                        fontName,
                                                        maxFontSize,
                                                        inputFile,
                                                        boringWordsFile,
                                                        outputFile);
            });
            app.Execute(args);

            return tagCloudSettings;
        }

        private static void CreateTagCloud(ITagCloudCreatorFactory tagCloudCreatorFactory, TagCloudSettings tagCloudSettings)
        {
            var bitmap = GetCloudImage(tagCloudCreatorFactory,
                                       tagCloudSettings.PictureSize,
                                       tagCloudSettings.CloudCenter,
                                       tagCloudSettings.Colors,
                                       tagCloudSettings.FontName,
                                       tagCloudSettings.MaxFontSize,
                                       tagCloudSettings.InputFile,
                                       tagCloudSettings.BoringWordsFile);

            bitmap.Save(tagCloudSettings.OutputFile);
        }

        private static Bitmap GetCloudImage(ITagCloudCreatorFactory tagCloudCreatorFactory, Size pictureSize,
                                            Point cloudCenter, Color[] colors, string fontName,
                                            int maxFontSize, string inputFile, string boringWordsFile)
        {
            var tagCloudCreator = tagCloudCreatorFactory
                .Get(pictureSize,
                     cloudCenter,
                     colors,
                     fontName,
                     maxFontSize,
                     inputFile,
                     boringWordsFile);
            var bitmap = tagCloudCreator.GetCloud();
            return bitmap;
        }
    }
}