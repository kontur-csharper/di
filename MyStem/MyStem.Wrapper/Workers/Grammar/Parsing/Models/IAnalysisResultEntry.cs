namespace MyStem.Wrapper.Workers.Grammar.Parsing.Models
{
    public interface IAnalysisResultEntry
    {
        string Lexeme { get; }
        MyStemSpeechPart SpeechPart { get; }
        MyStemWordQuality Quality { get; }
    }
}