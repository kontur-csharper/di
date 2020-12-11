using TagsCloud.App;

namespace TagsCloud.Infrastructure
{
    public interface IClient
    {
        string[] GetAvailableCommandName();
        void Run();
        Result<ICommand> FindCommandByName(string name);
    }
}