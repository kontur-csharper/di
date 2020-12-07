using System.Linq;
using FluentAssertions;
using MyStem.Wrapper.Workers.Grammar.Parsing;
using MyStem.Wrapper.Workers.Grammar.Parsing.Models;
using MyStem.Wrapper.Workers.Grammar.Raw;
using NUnit.Framework;

namespace MyStem.Wrapper.Tests
{
    // ReSharper disable once InconsistentNaming
    public class GrammarParser_Should
    {
        private IGrammarAnalysisParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new GrammarAnalysisParser(Enumerable.Empty<IAnalysisEntryParser>());
        }

        [TestCase("A", MyStemSpeechPart.Adjective, TestName = "Adjective")]
        [TestCase("ADV", MyStemSpeechPart.Adverb, TestName = "Adverb")]
        [TestCase("ADVPRO", MyStemSpeechPart.PronominalAdverb, TestName = "PronominalAdverb")]
        [TestCase("ANUM", MyStemSpeechPart.PronounNumeral, TestName = "PronounNumeral")]
        [TestCase("APRO", MyStemSpeechPart.PronounAdjective, TestName = "PronounAdjective")]
        [TestCase("COM", MyStemSpeechPart.CompositeWordPart, TestName = "CompositeWordPart")]
        [TestCase("CONJ", MyStemSpeechPart.Union, TestName = "Union")]
        [TestCase("INTJ", MyStemSpeechPart.Interjection, TestName = "Interjection")]
        [TestCase("NUM", MyStemSpeechPart.Numeral, TestName = "Numeral")]
        [TestCase("PART", MyStemSpeechPart.Particle, TestName = "Particle")]
        [TestCase("PR", MyStemSpeechPart.Pretext, TestName = "Pretext")]
        [TestCase("S", MyStemSpeechPart.Noun, TestName = "Noun")]
        [TestCase("SPRO", MyStemSpeechPart.Pronoun, TestName = "Pronoun")]
        [TestCase("V", MyStemSpeechPart.Verb, TestName = "Verb")]
        public void WordRecognized_ParseSpeechPart(string rawGrammarPrefix, MyStemSpeechPart expected)
        {
            var analysisResultRaw = new AnalysisResultRaw
            {
                Text = "ololo",
                Entries = new[]
                {
                    new AnalysisResultEntryRaw
                    {
                        Lexeme = "ololo",
                        Grammar = rawGrammarPrefix + "asdfv"
                    },
                }
            };

            parser.Parse(analysisResultRaw).Entries
                .Should()
                .ContainSingle()
                .Which
                .SpeechPart
                .Should()
                .Be(expected);
        }
    }
}