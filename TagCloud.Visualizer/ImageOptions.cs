using CommandLine;

namespace TagCloud.Visualizer
{
    public class ImageOptions
    {
        [Option('s', "fontSize")] public float FontSize { get; private set; }

        [Option('n', "fontName")] public string FontName { get; private set; }

        [Option('h', "height")] public int ImageHeight { get; private set; }

        [Option('w', "width")] public int ImageWidth { get; private set; }

        [Option('c', "color")] public string ColorName { get; private set; }

        [Option('i', "imageSaveName")] public string ImageSaveName { get; private set; }

        public ImageOptions()
        {
            FontSize = 12f;
            FontName = "Times New Roman";
            ImageHeight = 1000;
            ImageWidth = 1000;
            ColorName = "Black";
            ImageSaveName = "result.png";
        }

        public int ChangeImageOptionsAndReturnExitCode(ImageOptions opts)
        {
            FontSize = opts.FontSize;
            FontName = opts.FontName;
            ImageHeight = opts.ImageHeight;
            ImageWidth = opts.ImageWidth;
            ColorName = opts.ColorName;
            ImageSaveName = opts.ImageSaveName;
            return 0;
        }
    }
}