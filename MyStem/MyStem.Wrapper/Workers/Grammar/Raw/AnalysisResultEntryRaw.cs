using Newtonsoft.Json;

namespace MyStem.Wrapper.Workers.Grammar.Raw
{
    /// <summary>
    /// Unparsed analysis result entry
    /// </summary>
    public class AnalysisResultEntryRaw
    {
        [JsonProperty("lex", Required = Required.Default)]
        public string Lexeme { get; set; }

        [JsonProperty("gr", Required = Required.Default)]
        public string Grammar { get; set; }

        [JsonProperty("qual", Required = Required.Default)]
        public string Quality { get; set; }

        public override string ToString() => $"{nameof(Lexeme)}={Lexeme ?? "<null>"}, " +
                                             $"{nameof(Grammar)}={Grammar ?? "<null>"}, " +
                                             $"{nameof(Quality)}={Quality ?? "<null>"}";
    }
}