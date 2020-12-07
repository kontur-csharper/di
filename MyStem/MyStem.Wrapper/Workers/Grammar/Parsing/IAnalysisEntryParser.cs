using MyStem.Wrapper.Workers.Grammar.Parsing.Models;

namespace MyStem.Wrapper.Workers.Grammar.Parsing
{
    public interface IAnalysisEntryParser
    {
        MyStemSpeechPart ParserFor { get; }
        IAnalysisResultEntry Parse(AnalysisResultEntryData entryData);
    }
}