using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using TagCloud.Settings;
using YandexMystem.Wrapper.Enums;

namespace TagCloud.UserInterfaces
{
    public class CommandLineInterface
    {
        public readonly CircularLayouterSettings LayouterSettings;
        public readonly DrawerSettings DrawerSettings;
        public readonly FileReaderSettings FileReaderSettings;
        public readonly SaverSettings SaverSettings;
        public readonly string[] BoringWords;
        public readonly GramPartsEnum[] GramParts;

        public CommandLineInterface(string[] args)
        {
            var arguments = Parser.Default.ParseArguments<CLIArguments>(args).Value;
            
            LayouterSettings = new CircularLayouterSettings(
                new Point(arguments.CenterX, arguments.CenterY),
                arguments.SpiralPitch,
                arguments.SpiralStep);

            FileReaderSettings = new FileReaderSettings(arguments.FilePath);
            
            DrawerSettings = new DrawerSettings();
            MakeDrawerSettings(arguments);
            
            SaverSettings = new SaverSettings(arguments.OutputPath, arguments.OutputFileName, arguments.Extension);

            GramParts = arguments.GramParts.ToArray();
            BoringWords = arguments.BoringWords.ToArray();
        }

        private void MakeDrawerSettings(CLIArguments arguments)
        {
            DrawerSettings.ImageSize = new Size(arguments.Width, arguments.Height);
            DrawerSettings.Colors = arguments.Colors.Select(colorStr => ParseColorFromRGBString(colorStr)).ToList();
            DrawerSettings.BackgroundColor = ParseColorFromRGBString(arguments.BackgroundColor);
            if (arguments.ForegroundColor != null)
                DrawerSettings.ForegroundColor = ParseColorFromRGBString(arguments.ForegroundColor);
            DrawerSettings.FontFamily = arguments.FontFamily;
            DrawerSettings.MinFontSize = arguments.MinFontSize;
            DrawerSettings.MaxFontSize = arguments.MaxFontSize;
            DrawerSettings.OrderByWeight = arguments.OrderByWeight;
        }

        private Color ParseColorFromRGBString(string colorAsString)
        {
            var rgbAsArray = colorAsString.Split()
                .Select(s => Convert.ToInt32(s)).ToArray();
            if (rgbAsArray.Length != 3 || rgbAsArray.All(n => n < 0 || n > 255))
                throw new ArgumentException("Incorrect Color format");
            return Color.FromArgb(rgbAsArray[0], rgbAsArray[1], rgbAsArray[2]);
        }
    }
}