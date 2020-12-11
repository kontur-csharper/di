using System;
using System.IO;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class ConsoleClient : IClient
    {
        private readonly ICommand[] commands;
        private readonly TextWriter writer;

        public ConsoleClient(ICommand[] commands, TextWriter writer)
        {
            this.commands = commands;
            this.writer = writer;
        }

        public string[] GetAvailableCommandName() => commands.Select(x => x.Name).ToArray();

        public void Run()
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (line == null || line == "exit") return;
                Execute(line.Split(' '));
            }
        }

        public Result<ICommand> FindCommandByName(string name) =>
            commands
                .FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase))?.AsResult()
            ?? Result.Fail<ICommand>($"Not a command '{name}'. Write 'h' to find out the available commands");


        private void Execute(string[] args) =>
            ValidateFirstArg(args)
                .Then(FindCommandByName)
                .Then(x => x.Execute(args.Skip(1).ToArray()))
                .OnFail(writer.WriteLine);

        private Result<string> ValidateFirstArg(string[] args) =>
            args.Length == 0 || args[0].Length == 0
                ? Result.Fail<string>("Please specify <command> as the first command line argument")
                : args[0];
    }
}