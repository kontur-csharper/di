using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Commands;

namespace TagCloud.Controllers
{
    public class ConsoleController : IController
    {
        private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        public ConsoleController(ICommand[] commands)
        {
            foreach (var command in commands)
                AddCommand(command);
        }

        public void Start()
        {
            while (true)
            {
                Console.Write("> ");
                var args = Console.ReadLine()?
                    .Trim()
                    .Split(' ')
                    .Where(x => x.Length > 0)
                    .ToArray();
                if (args == null || args.Length == 0)
                    continue;
                var command = FindCommand(args[0]);
                if (command == null)
                {
                    Console.WriteLine("This is not a command.\nFor example: 'help'");
                    continue;
                }

                var result = command.Handle(args.Skip(1).ToArray());
                Console.WriteLine(result.Message);
                if (!result.Success)
                    Console.WriteLine($"Usage: {command.Usage}");
            }
        }

        private ICommand FindCommand(string commandId)
        {
            return commands.TryGetValue(commandId, out var command) ? command : null;
        }

        private void AddCommand(ICommand command)
        {
            commands.Add(command.CommandId, command);
        }
    }
}
