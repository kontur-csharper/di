using TagCloud.Settings;

namespace TagCloud.Commands
{
    public class IgnoreCommand : ICommand
    {
        private readonly SourceSettings sourceSettings;

        public IgnoreCommand(SourceSettings sourceSettings)
        {
            this.sourceSettings = sourceSettings;
            Usage = $"{CommandId} <add/remove> <word>, or without args";
        }

        public string CommandId { get; } = "ignore";
        public string Description { get; } = "Allows to ignore the specify word";
        public string Usage { get; }

        public ICommandResult Handle(string[] args)
        {
            if (args.Length == 0)
                return AllIgnored();
            if (args.Length != 2)
                return CommandResult.WithNoArgs();
            var action = args[0];
            var word = args[1];
            if (action == "add")
                sourceSettings.Ignore.Add(word);
            else
                sourceSettings.Ignore.Remove(word);
            return CommandResult.WithSuccess();
        }

        private ICommandResult AllIgnored()
        {
            return new CommandResult(true, $"Ignore words: {string.Join(", ", sourceSettings.Ignore)}");
        }
    }
}
