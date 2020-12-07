using MyStem.Wrapper.Workers.Grammar.Parsing.Models;
using MyStem.Wrapper.Workers.Grammar.Raw;

namespace MyStem.Wrapper.Workers.Grammar.Parsing
{
    public interface IGrammarAnalysisParser
    {
        AnalysisResult Parse(AnalysisResultRaw raw);
    }
}