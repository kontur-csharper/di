using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class SetImageSizeCommand : ICommand
    {
        private readonly IImageSizeProvider imageSizeProvider;

        public SetImageSizeCommand(IImageSizeProvider imageSizeProvider)
        {
            this.imageSizeProvider = imageSizeProvider;
        }

        public string Name { get; } = "setsize";
        public string Description { get; } = "setsize <width> <height>      # setting tag cloud size";

        public Result<None> Execute(string[] args) =>
            ValidateArgs(args)
                .Then(x =>
                {
                    imageSizeProvider.ImageSize.Width = x[0];
                    imageSizeProvider.ImageSize.Height = x[1];
                });

        private Result<int[]> ValidateArgs(string[] args) =>
            args.Length < 2
                ? Result.Fail<int[]>(
                    "Invalid number of arguments. Write 'help setsize' to see more information")
                : Result.Of(() => args.Select(int.Parse).ToArray(), "Could not read number");
    }
}