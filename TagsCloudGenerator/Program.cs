using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Autofac;
using CommandLine;
using NHunspell;
using TagsCloudGenerator.ConsoleUI;
using TagsCloudGenerator.Core.Drawers;
using TagsCloudGenerator.Core.Filters;
using TagsCloudGenerator.Core.Layouters;
using TagsCloudGenerator.Core.Normalizers;
using TagsCloudGenerator.Core.Spirals;
using TagsCloudGenerator.Core.Translators;
using TagsCloudGenerator.Infrastructure;

namespace TagsCloudGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run);
        }

        private static void Run(Options options)
        {
            var translator = TextToTagsTranslatorFactory.Create(options.Alpha, options.Phi);
            var tags = translator.TranslateTextToTags(
                File.ReadLines(options.InputFilename),
                new HashSet<string>(),
                options.FontFamily,
                options.MinFontSize);

            var palette = options.ColorTheme.Palette();
            var cloudDrawer =
                new RectangleCloudDrawer(palette.BackgroundColor, new SolidBrush(palette.PrimaryColor));
            var bitmap = cloudDrawer.DrawCloud(tags.ToList());
            bitmap = ImageUtils.ResizeImage(bitmap, options.Width, options.Height);
            bitmap.Save(options.OutputFilename, ImageFormatUtils.GetImageFormatByExtension(options.ImageExtension));
        }
    }
}