using System.Text;

namespace TagCloud.Commands
{
    public class HelpCommand : ICommand
    {
        public ICommand[] Commands { get; set; }
        public string CommandId { get; } = "help";
        public string Description { get; } = "Show all commands";
        public string Usage { get; } = "help";

        public ICommandResult Handle(string[] args)
        {
            var message = new StringBuilder();
            foreach (var command in Commands)
            {
                message.AppendLine($"{command.CommandId}:");
                message.AppendLine($"\tDescription: {command.Description}");
                message.AppendLine($"\tUsage: {command.Usage}");
            }

            return new CommandResult(true, message.ToString());
        }
    }
}
