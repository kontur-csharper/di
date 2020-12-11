using System;
using System.IO;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class DetailedHelpCommand : ICommand
    {
        private readonly Lazy<IClient> client;
        private readonly TextWriter writer;

        public DetailedHelpCommand(Lazy<IClient> client, TextWriter writer)
        {
            this.client = client;
            this.writer = writer;
        }

        public string Name { get; } = "help";

        public string Description { get; } = "help <command>      # prints help for <command>";

        public Result<None> Execute(string[] args) =>
            ValidateArgs(args)
                .Then(x => client.Value.FindCommandByName(x))
                .Then(x => writer.WriteLine(x.Description));

        private Result<string> ValidateArgs(string[] args) =>
            args.Length == 0
                ? Result.Fail<string>(
                    "Invalid number of arguments. Write 'help help' to see more information")
                : args[0];
    }
}