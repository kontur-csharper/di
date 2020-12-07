using MyStem.Wrapper.Workers.Grammar.Raw;

namespace MyStem.Wrapper.Workers.Grammar
{
    public interface IGrammarAnalyser
    {
        AnalysisResultRaw[] GetRawResult(string text);
    }
}