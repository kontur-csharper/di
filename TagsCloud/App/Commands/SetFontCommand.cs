using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class SetFontCommand : ICommand
    {
        private readonly IFontFamilyProvider fontFamilyProvider;

        public SetFontCommand(IFontFamilyProvider fontFamilyProvider)
        {
            this.fontFamilyProvider = fontFamilyProvider;
        }

        public string Name { get; } = "setfont";
        public string Description { get; } = "setfont      # setting font for tag cloud";

        public Result<None> Execute(string[] args) =>
            Result
                .Of(() => new FontFamily(string.Join(" ", args)), "Could not find this font")
                .Then(x => { fontFamilyProvider.FontFamily = x; });
    }
}