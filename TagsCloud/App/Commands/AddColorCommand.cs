using System;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class AddColorCommand : ICommand
    {
        private readonly IImageColorProvider colorSettingsProvider;

        public AddColorCommand(IImageColorProvider colorSettingsProvider)
        {
            this.colorSettingsProvider = colorSettingsProvider;
        }

        public string Name { get; } = "addcolor";
        public string Description { get; } = "addcolor params <color>      # Adding colors for cloud tag";

        public Result<None> Execute(string[] args) =>
            Result
                .Of(() => args.Select(ParseColor))
                .Then(x => colorSettingsProvider.AddColors(x));

        private Color ParseColor(string name)
        {
            var color = Color.FromName(name);
            if (!color.IsKnownColor)
                throw new InvalidOperationException($"Could not read color '{name}'");
            return color;
        }
    }
}