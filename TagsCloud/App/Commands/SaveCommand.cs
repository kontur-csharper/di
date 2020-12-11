using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class SaveCommand : ICommand
    {
        private readonly IImageHolder imageHolder;

        public SaveCommand(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public string Name { get; } = "save";
        public string Description { get; } = "save <fileName>      # save tag cloud image";

        public Result<None> Execute(string[] args) => imageHolder.SaveImage(string.Join(" ", args));
    }
}