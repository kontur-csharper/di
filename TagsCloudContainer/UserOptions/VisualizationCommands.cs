using System.Drawing;
using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class ColorsCommands : StorageCommands
    {
        [Required, Name("t", "textColor"), Description("Text color")]
        public KnownColor TextColor { get; set; }

        [Required, Name("b", "backColor"), Description("Background color")]
        public KnownColor BackgroundColor { get; set; }

        [Required, Name("f", "font"), Description("Text font, space separated font name and size")]
        public string[] Font { get; set; }

        [Required, Name("s", "size"), Description("Image size with int parameters, space separated width and height")]
        public int[] ImageSize { get; set; }
    }
}