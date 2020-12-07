namespace MyStem.Wrapper.Workers.Grammar.Parsing.Models
{
    public class DefaultAnalysisResultEntry : IAnalysisResultEntry
    {
        public DefaultAnalysisResultEntry(MyStemSpeechPart speechPart, string lexeme, MyStemWordQuality quality)
        {
            SpeechPart = speechPart;
            Lexeme = lexeme;
            Quality = quality;
        }

        public string Lexeme { get; }
        public MyStemSpeechPart SpeechPart { get; }
        public MyStemWordQuality Quality { get; }
    }
}