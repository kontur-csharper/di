namespace TagCloud.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }

        public static CommandResult WithNoArgs()
        {
            return new CommandResult(false, "One or more required parameters not specified");
        }

        public static CommandResult WithSuccess()
        {
            return new CommandResult(true, "Success");
        }
    }
}
