using System.Collections.Generic;
using System.Linq;
using MyStem.Wrapper.Workers.Grammar.Parsing.Models;
using MyStem.Wrapper.Workers.Grammar.Raw;

namespace MyStem.Wrapper.Workers.Grammar.Parsing
{
    public class GrammarAnalysisParser : IGrammarAnalysisParser
    {
        private readonly Dictionary<MyStemSpeechPart, IAnalysisEntryParser> parsers;

        private readonly Dictionary<string, MyStemSpeechPart> speechParts = new Dictionary<string, MyStemSpeechPart>
        {
            // ReSharper disable StringLiteralTypo
            {"A", MyStemSpeechPart.Adjective},
            {"ADV", MyStemSpeechPart.Adverb},
            {"ADVPRO", MyStemSpeechPart.PronominalAdverb},
            {"ANUM", MyStemSpeechPart.PronounNumeral},
            {"APRO", MyStemSpeechPart.PronounAdjective},
            {"COM", MyStemSpeechPart.CompositeWordPart},
            {"CONJ", MyStemSpeechPart.Union},
            {"INTJ", MyStemSpeechPart.Interjection},
            {"NUM", MyStemSpeechPart.Numeral},
            {"PART", MyStemSpeechPart.Particle},
            {"PR", MyStemSpeechPart.Pretext},
            {"S", MyStemSpeechPart.Noun},
            {"SPRO", MyStemSpeechPart.Pronoun},
            {"V", MyStemSpeechPart.Verb}
            // ReSharper restore StringLiteralTypo
        };

        public GrammarAnalysisParser(IEnumerable<IAnalysisEntryParser> parsers)
        {
            this.parsers = parsers.ToDictionary(x => x.ParserFor);
        }

        public AnalysisResult Parse(AnalysisResultRaw raw) =>
            new AnalysisResult
            {
                Text = raw.Text,
                Entries = raw.Entries.Select(ParseEntry).ToList()
            };

        private IAnalysisResultEntry ParseEntry(AnalysisResultEntryRaw rawEntry)
        {
            var speechPart = speechParts.Where(x => rawEntry.Grammar.StartsWith(x.Key))
                .OrderByDescending(x => x.Key.Length)
                .Select(x => x.Value)
                .FirstOrDefault();

            var myStemWordQuality = GetQuality(rawEntry);
            return parsers.TryGetValue(speechPart, out var parser)
                ? parser.Parse(new AnalysisResultEntryData(rawEntry.Grammar, myStemWordQuality, speechPart))
                : new DefaultAnalysisResultEntry(speechPart, rawEntry.Lexeme, myStemWordQuality);
        }

        private static MyStemWordQuality GetQuality(AnalysisResultEntryRaw rawEntry) => rawEntry.Quality switch
        {
            "bastard" => MyStemWordQuality.Bastard,
            _ => MyStemWordQuality.None
        };
    }
}