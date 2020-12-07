using MyStem.Wrapper.Workers.Grammar.Parsing;
using MyStem.Wrapper.Workers.Grammar.Parsing.Models;
using MyStem.Wrapper.Workers.Grammar.Raw;

namespace MyStem.Wrapper.Workers.Grammar
{
    public static class Extensions
    {
        public static AnalysisResult ParseWith(this AnalysisResultRaw raw, IGrammarAnalysisParser parser) =>
            parser.Parse(raw);
    }
}