using MatthiWare.CommandLine;

namespace Cloud.ClientUI
{
    public class ArgumentParser : IArgumentParser
    {
        public Arguments Parse(string[] args)
        {
            var parser = new CommandLineParser<Arguments>();
            return parser.Parse(args).Result;
        }
    }
}