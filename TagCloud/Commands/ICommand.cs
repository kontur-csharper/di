namespace TagCloud.Commands
{
    public interface ICommand
    {
        string CommandId { get; }
        string Description { get; }
        string Usage { get; }
        ICommandResult Handle(string[] args);
    }
}
