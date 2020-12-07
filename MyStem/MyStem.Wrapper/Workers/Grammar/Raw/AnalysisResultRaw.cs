using System.Linq;
using Newtonsoft.Json;

namespace MyStem.Wrapper.Workers.Grammar.Raw
{
    /// <summary>
    /// Unparsed analysis result for word
    /// </summary>
    public class AnalysisResultRaw
    {
        [JsonProperty("text", Required = Required.DisallowNull)]
        public string Text { get; set; }

        [JsonProperty("analysis", Required = Required.DisallowNull)]
        public AnalysisResultEntryRaw[] Entries { get; set; }

        public override string ToString()
        {
            var entriesStr = Entries == null
                ? "<null>"
                : $"[{string.Join(", ", Entries.Select(e => $"<{e}>"))}]";
            return $"{nameof(Text)}={Text}, {nameof(Entries)}={entriesStr}";
        }
    }
}