using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.UserOptions;

namespace TagsCloudContainer.ConvertersAndCheckers
{
    public class TemporarySettingsStorage
    {
        private static readonly IDirectoryChecker _directoryChecker = new DirectoryExistsChecker();
        private static readonly IFileExistsСhecker _fileExistsСhecker = new FileExistsСhecker();
        private static readonly IFontConverter _fontConverter = new FontConverter();
        private static readonly IImageFormatConverter _imageFormatConverter = new ImageFormatConverter();
        private static readonly IImageSizeConverter _imageSizeConverter = new ImageSizeConverter();

        public string PathToCustomText { get; }

        public string PathToSave { get; }

        public ImageFormat ImageFormat { get; }

        public Font Font { get; }

        public Size ImageSize { get; }

        public Color TextColor { get; }

        public Color BackgroundColor { get; }

        public string[] BoringWords { get; }

        public double AdditionSpiralAngleFromDegrees { get; }

        public double SpiralStep { get; }


        private TemporarySettingsStorage(AllUserCommands commands)
        {
            PathToCustomText = _fileExistsСhecker.GetProvenPath(commands.PathToCustomText);
            PathToSave = _directoryChecker.GetExistingDirectory(commands.PathToSave);
            ImageFormat = _imageFormatConverter.ConvertToImageFormat(commands.ImageFormat);
            ImageSize = _imageSizeConverter.ConvertToSize(commands.ImageSize);
            BackgroundColor = Color.FromKnownColor(commands.BackgroundColor);
            TextColor = Color.FromKnownColor(commands.TextColor);
            Font = _fontConverter.ConvertToFont(commands.Font);
            BoringWords = commands.BoringWords;
            AdditionSpiralAngleFromDegrees = commands.AdditionSpiralAngleFromDegrees;
            SpiralStep = commands.SpiralStep;
        }

        public static TemporarySettingsStorage From(AllUserCommands commands) => new TemporarySettingsStorage(commands);
    }
}