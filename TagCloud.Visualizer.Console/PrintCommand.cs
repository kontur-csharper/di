using System.Collections.Generic;
using System.IO;
using CommandLine;
using TagCloud.ImageSaver;

namespace TagCloud.Visualizer.Console
{
    [Verb("print")]
    public class PrintCommand
    {
        private static readonly string SavePath = Path.Combine(Directory.GetCurrentDirectory(),
            "..",
            "..",
            "..",
            "..",
            $"{nameof(TagCloud)}.{nameof(Visualizer)}",
            "images");

        public static int PrintCloudAndReturnExitCode(ICloudLayouter cloudLayouter, IEnumerable<string> words,
            ImageOptions imageOptions, IImageSaver imageSaver)
        {
            var sizes = SizesCreator.CreateSizesArray(words, imageOptions.FontSize, imageOptions.FontName);
            var rects = cloudLayouter.GetRectangles(sizes);
            using var bitmap = BitmapCreator.DrawBitmap(rects, imageOptions);
            imageSaver.TrySaveImage(bitmap, SavePath, imageOptions);
            return 0;
        }
    }
}