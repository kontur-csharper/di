using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Autofac;
using Autofac.Core;
using CommandLine;
using HomeExercise.settings;

namespace HomeExercise
{
    public class ConsoleCloudClient : IConsoleCloudClient
    {
        public void HandleSettingsFromConsole(string[] args, ContainerBuilder builder)
        {
            Parser.Default
                .ParseArguments<Options.Options>(args)
                .WithParsed(options => 
            {
                HandleFilesOption(options.WordsPath, options.BoringPath, builder);
                HandleWordOption(options.Font, options.Coefficient, builder);
                HandleSpiralOption(options.CenterX, options.CenterY, builder);
                HandlePainterOption(options.Wight, options.Height, options.ImageName, options.Format, options.Color, builder);
            });
        }

        private void HandleFilesOption(string wordPath, string boringWordPath, ContainerBuilder builder)
        {
            builder.RegisterType<FileProcessor>().As<IFileProcessor>()
                .WithParameters(new Parameter[] 
                {new NamedParameter("pathWords", wordPath), 
                    new NamedParameter("pathBoringWords", boringWordPath) });
        }

        private void HandleWordOption(string fontText, int coefficient, ContainerBuilder builder)
        {
            FontFamily font;
            try
            { 
                font = new FontFamily(fontText);
            }
            catch (ArgumentException e)
            {
                font = new FontFamily("Microsoft Sans Serif");
            }
            var wordSettings = new WordSettings(font, coefficient);
            builder.RegisterInstance(wordSettings).As<WordSettings>();
        }
        
        private void HandleSpiralOption(int x, int y, ContainerBuilder builder)
        {
            var center = new Point(x,y);
            var spiralSettings = new SpiralSettings(center);
            builder.RegisterInstance(spiralSettings).As<SpiralSettings>();
        }

        private void HandlePainterOption(int width, int height, string fileName, string format, KnownColor knownColor, ContainerBuilder builder)
        {
            var resultFormat = GetImageFormat(format);
            var color = Color.FromKnownColor(knownColor);
            var painterSettings = new PainterSettings(width,height,fileName,resultFormat, color);
            builder.RegisterInstance(painterSettings).As<PainterSettings>();
        }
        
        private static ImageFormat GetImageFormat(string extension)  
        {
            var prop = typeof(ImageFormat)
                .GetProperties()
                .FirstOrDefault(p => p.Name.Equals(extension, StringComparison.InvariantCultureIgnoreCase));

            var result = prop != null
                ? prop.GetValue(prop) as ImageFormat
                : ImageFormat.Png;
    
            return result;  
        }
    }
}