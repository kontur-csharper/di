using System;
using System.Collections.Generic;
using MyStem.Wrapper.Workers.Grammar.Parsing.Models;
using TagCloud.Core.Output;
using TagCloud.Core.Text;
using TagCloud.Core.Text.Formatting;
using TagCloud.Core.Text.Preprocessing;
using TagCloud.Gui.ImageResizing;

namespace TagCloud.Gui
{
    public static class VisibleName
    {
        private static readonly Dictionary<Type, string> overridingNames = new Dictionary<Type, string>
        {
            {typeof(RandomFontSizeSource), "Random font size"},
            {typeof(BiggerAtCenterFontSizeSource), "Most frequent bigger and closer to center"},
            {typeof(BlacklistWordFilter), "Without \"boring\" words"},
            {typeof(MyStemWordsConverter), "Yadnex MyStem"},
            {typeof(LengthWordFilter), "Only with length more or equal to 3"},
            {typeof(LowerCaseConverter), "Lower cased"},
            {typeof(FileWordsReader), "Text file"},
            {typeof(FileResultWriter), "Save to file"},
            {typeof(DontModifyImageResizer), "Save as it is"},
            {typeof(FitToSizeImageResizer), "Fit to size"},
            {typeof(StretchImageResizer), "Stretch to size"},
            {typeof(PlaceAtCenterImageResizer), "Place at center or fit to size"}
        };

        public static string Get(Type type) =>
            overridingNames.TryGetValue(type, out var overriden)
                ? overriden
                : type.Name;

        public static string Get<T>(T enumItem) where T : struct, Enum
        {
            return enumItem switch
            {
                MyStemSpeechPart speechPart => Get(speechPart),
                FontSizeSourceType fontSizeSourceType => Get(fontSizeSourceType),
                _ => EnumDefaultName(enumItem)
            };
        }

        private static string Get(MyStemSpeechPart speechPart) => speechPart switch
        {
            MyStemSpeechPart.Unrecognized => "Unrecognized",
            MyStemSpeechPart.Adjective => "Adjective",
            MyStemSpeechPart.Adverb => "Adverb",
            MyStemSpeechPart.PronominalAdverb => "Pronominal Adverb",
            MyStemSpeechPart.PronounNumeral => "Pronoun Numeral",
            MyStemSpeechPart.PronounAdjective => "Pronoun Adjective",
            MyStemSpeechPart.CompositeWordPart => "Part of Composite word",
            MyStemSpeechPart.Union => "Union",
            MyStemSpeechPart.Interjection => "Interjection",
            MyStemSpeechPart.Numeral => "Numeral",
            MyStemSpeechPart.Particle => "Particle",
            MyStemSpeechPart.Pretext => "Pretext",
            MyStemSpeechPart.Noun => "Noun",
            MyStemSpeechPart.Pronoun => "Pronoun",
            MyStemSpeechPart.Verb => "Verb",
            _ => EnumDefaultName(speechPart)
        };

        private static string Get(FontSizeSourceType sizeSourceType) => sizeSourceType switch
        {
            FontSizeSourceType.Random => "Randomized",
            FontSizeSourceType.FrequentIsBigger => "More frequent is bigger",
            _ => EnumDefaultName(sizeSourceType)
        };

        private static string EnumDefaultName<T>(T enumItem) where T : struct, Enum => enumItem.ToString();
    }
}