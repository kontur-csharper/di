using TagsCloud.App;

namespace TagsCloud.Infrastructure
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Result<None> Execute(string[] args);
    }
}