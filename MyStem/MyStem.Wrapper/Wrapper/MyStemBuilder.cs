using System;
using System.Linq;
using MyStem.Wrapper.Enums;

namespace MyStem.Wrapper.Wrapper
{
    public sealed class MyStemBuilder : IMyStemBuilder
    {
        private readonly string path;

        public MyStemBuilder(string path)
        {
            this.path = path;
        }

        public IMyStem Create(MyStemOutputFormat outputFormat, params MyStemOptions[] args)
        {
            var argsEnumerable = args.Select(OptionToExecutionArg)
                .Prepend(OutputFormatToExecutionArg(outputFormat));
            var launchArgs = string.Join(" ", argsEnumerable);
            return new MyStem(path, launchArgs);
        }

        private static string OptionToExecutionArg(MyStemOptions option) => option switch
        {
            MyStemOptions.LinearMode => "-n",
            MyStemOptions.CopyEverything => "-c",
            MyStemOptions.OnlyLexical => "-w",
            MyStemOptions.WithoutOriginalForm => "-l",
            MyStemOptions.WithGrammarInfo => "-i",
            MyStemOptions.JoinSingleLemmaWordForms => "-g",
            MyStemOptions.PrintEndOfSentenceMarker => "-s",
            MyStemOptions.WithContextualDeHomonymy => "-d",
            _ => throw new ArgumentOutOfRangeException(nameof(option),
                $"Unsupported {nameof(MyStemOptions)} {option}")
        };

        private static string OutputFormatToExecutionArg(MyStemOutputFormat format) => format switch
        {
            MyStemOutputFormat.Json => "--format json",
            MyStemOutputFormat.Xml => "--format xml",
            MyStemOutputFormat.Text => "--format text",
            _ => throw new ArgumentOutOfRangeException(nameof(format),
                $"Unsupported {nameof(MyStemOutputFormat)} {format}")
        };
    }
}