using System.Collections.Generic;

namespace MyStem.Wrapper.Workers.Grammar.Parsing.Models
{
    public class AnalysisResult
    {
        public string Text { get; set; }
        public IList<IAnalysisResultEntry> Entries { get; set; } = new List<IAnalysisResultEntry>();
    }
}