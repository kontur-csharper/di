using System.Collections.Generic;
using System.Linq;
using MyStem.Wrapper.Workers.Grammar;
using MyStem.Wrapper.Workers.Grammar.Parsing;
using MyStem.Wrapper.Workers.Grammar.Parsing.Models;

namespace TagCloud.Core.Text.Preprocessing
{
    public class MyStemSpeechPartWordsFilter : ISpeechPartWordsFilter
    {
        private readonly IGrammarAnalyser analyser;
        private readonly IGrammarAnalysisParser parser;

        public MyStemSpeechPartWordsFilter(IGrammarAnalyser analyser, IGrammarAnalysisParser parser)
        {
            this.analyser = analyser;
            this.parser = parser;
        }

        public IEnumerable<string> OnlyWithSpeechPart(
            IEnumerable<string> words,
            ISet<MyStemSpeechPart> includedSpeechParts) =>
            analyser.GetRawResult(string.Join(" ", words))
                .Select(r => r.ParseWith(parser))
                .Where(r => r.Entries.Any(e => includedSpeechParts.Contains(e.SpeechPart)))
                .Select(r => r.Text);
    }
}