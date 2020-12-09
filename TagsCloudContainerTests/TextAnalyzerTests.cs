using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TagsCloudContainer.App;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.App.TextAnalyzer;
using TagsCloudContainer.Infrastructure.TextAnalyzer;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudContainerTests
{
    class TextAnalyzerTests
    {
        private readonly ITextAnalyzer textAnalyzer;
        private readonly Mysteam mysteam;
        private readonly FilteringWordsSettings filteringSettings;

        public TextAnalyzerTests()
        {
            var serviceProvider = Program.GetAppServiceProvider();
            textAnalyzer = serviceProvider.GetRequiredService<ITextAnalyzer>();
            mysteam = serviceProvider.GetRequiredService<Mysteam>();
            filteringSettings = FilteringWordsSettings.Instance;
        }

        [Test]
        public void LiteratureTextParser_ShouldParseTextToWords()
        {
            new LiteratureTextParser()
                .GetWords(new[] {"строка с символами, словами и не 6675 словами"})
                .ToArray()
                .Should()
                .BeEquivalentTo("строка", "с", "символами", "словами", "и", "не", "словами");
        }

        [TestCase(GramPartsEnum.Conjunction, "и")]
        [TestCase(GramPartsEnum.Interjection, "ах")]
        [TestCase(GramPartsEnum.NounPronoun, "он")]
        public void PartOfSpeechFilter_ShouldFilterPOS_IfPOSAreBoring(GramPartsEnum gramPart, string word)
        {
            filteringSettings.BoringGramParts = new[] {gramPart}.ToImmutableHashSet();
            new PartOfSpeechFilter(filteringSettings, mysteam).IsBoring(word).Should().BeTrue();
        }

        [TestCase(GramPartsEnum.Conjunction, "и")]
        [TestCase(GramPartsEnum.Interjection, "ах")]
        [TestCase(GramPartsEnum.NounPronoun, "он")]
        public void PartOfSpeechFilter_ShouldNotFilterPOS_IfPOSAreNotBoring(GramPartsEnum gramPart, string word)
        {
            filteringSettings.BoringGramParts = filteringSettings.BoringGramParts.Except(new[] {gramPart});
            new PartOfSpeechFilter(filteringSettings, mysteam).IsBoring(word).Should().BeFalse();
        }

        [TestCase("слова", "слово")]
        [TestCase("слов", "слово")]
        [TestCase("слову", "слово")]
        [TestCase("словам", "слово")]
        public void ToInitialFormNormalizer_ShouldReturnInitialFormOfWord(string word, string initialForm)
        {
            new ToInitialFormNormalizer(mysteam).NormalizeWord(word).Should().BeEquivalentTo(initialForm);
        }

        [Test]
        public void TextAnalyzer_ShouldReturnCorrectFrequencyDictionary()
        {
            var text = new[] {"слова", "слово", "количество"};
            textAnalyzer
                .GenerateFrequencyDictionary(text)
                .Should()
                .HaveCount(2)
                .And.ContainKeys("слово", "количество").And.ContainValues(2.0 / 3, 1.0 / 3);
        }
    }
}
