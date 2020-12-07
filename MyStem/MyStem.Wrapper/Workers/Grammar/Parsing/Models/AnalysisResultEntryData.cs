namespace MyStem.Wrapper.Workers.Grammar.Parsing.Models
{
    public readonly struct AnalysisResultEntryData
    {
        public readonly string RawGrammar;
        public readonly MyStemWordQuality WordQuality;
        public readonly MyStemSpeechPart SpeechPart;

        public AnalysisResultEntryData(string rawGrammar, MyStemWordQuality wordQuality, MyStemSpeechPart speechPart)
        {
            RawGrammar = rawGrammar;
            WordQuality = wordQuality;
            SpeechPart = speechPart;
        }
    }
}