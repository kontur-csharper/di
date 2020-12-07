using TagCloud.Settings;

namespace TagCloud.Commands
{
    public class DebugCommand : ICommand
    {
        private readonly SourceSettings settings;

        public DebugCommand(SourceSettings sourceSettings)
        {
            settings = sourceSettings;
        }

        public string CommandId { get; } = "debug";
        public string Description { get; } = "Just for debug";
        public string Usage { get; } = "debug";

        public ICommandResult Handle(string[] args)
        {
            return new CommandResult(true, settings.Destination);
        }
    }
}
