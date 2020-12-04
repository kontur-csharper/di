using System.Drawing;
using McMaster.Extensions.CommandLineUtils;

namespace TagCloud
{
    public class CommandLineInterface
    {
        public Color StringColor { get; private set; }
        public FontFamily StringFont { get; private set; }
        public string FileName { get; private set; }
        public Size CanvasSize { get; private set; }
        public Background BackgroundType { get; private set; }

        public void ConfigureCLI(CommandLineApplication app)
        {
            app.HelpOption();
            var optionInput = app.Option("-i|--input <INPUT>", "input filename", CommandOptionType.SingleValue);
            var optionFont = app.Option("-f|--font <FONT>", "font family", CommandOptionType.SingleValue);
            var optionSize = app.Option("-s|--size <SIZE>", "size of image width,height", CommandOptionType.SingleValue);
            var optionBackground = app.Option("-b|--backgound <BACKGROUND_STYLE>", "background style rectangles|empty|circle", CommandOptionType.SingleValue);
            var optionStringColor = app.Option("-c|--color <COLOR>", "string color r,g,b", CommandOptionType.SingleValue);
            
            app.OnExecute(() =>
            {
                CanvasSize = optionSize.HasValue() ? ArgumentParser.GetSize(optionSize.Value()) : new Size(1000, 800);
                BackgroundType = optionBackground.HasValue() ? ArgumentParser.GetBackground(optionBackground.Value()) : Background.Empty;
                FileName = optionInput.HasValue() ? ArgumentParser.CheckFileName(optionInput.Value()) : "input.txt";
                StringFont = optionFont.HasValue() ? ArgumentParser.GetFont(optionFont.Value()) : new FontFamily("Arial");
                StringColor = optionStringColor.HasValue() ? ArgumentParser.ParseColor(optionStringColor.Value()) : Color.Black;

                return 1;
            });
        }
    }
}