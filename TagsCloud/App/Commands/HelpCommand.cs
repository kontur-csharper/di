using System;
using System.IO;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly Lazy<IClient> client;
        private readonly TextWriter writer;

        public HelpCommand(Lazy<IClient> client, TextWriter writer)
        {
            this.client = client;
            this.writer = writer;
        }

        public string Name { get; } = "h";
        public string Description { get; } = "h      # prints available commands list";

        public Result<None> Execute(string[] args) =>
            Result.OfAction(() =>
                writer.WriteLine("Available commands: " + string.Join(", ", client.Value.GetAvailableCommandName())));
    }
}