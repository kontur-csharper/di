using System.IO;
using TagCloud.Settings;

namespace TagCloud.Commands
{
    public class SourceCommand : ICommand
    {
        private readonly SourceSettings sourceSettings;

        public SourceCommand(SourceSettings sourceSettings)
        {
            this.sourceSettings = sourceSettings;
        }

        public string CommandId { get; } = "source";
        public string Description { get; } = "Allows to specify the path for the word file";
        public string Usage { get; } = "source <Full file name>";

        public ICommandResult Handle(string[] args)
        {
            if (args.Length != 1)
                return new CommandResult(false, "You must specify the path to the file");
            var destination = args[0];
            if (!File.Exists(destination))
                return new CommandResult(false, "Path to the file is incorrect or doesn't exists");
            sourceSettings.Destination = destination;
            return new CommandResult(true, "Success");
        }
    }
}
