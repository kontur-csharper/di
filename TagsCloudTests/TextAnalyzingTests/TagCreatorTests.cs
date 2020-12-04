using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Common;
using TagsCloudContainer.TextAnalyzing;

namespace TagsCloudTests.TextAnalyzingTests
{
    internal class TagCreatorTests
    {
        private FilesSettings filesSettings;
        private FontSettings fontSettings;
        private ImageSettings imageSettings;

        [SetUp]
        public void SetUp()
        {
            fontSettings = new FontSettings();
            imageSettings = new ImageSettings();
            filesSettings = new FilesSettings();
            filesSettings.TextFilePath = @"..\..\..\testTextAnalyzer.txt";
            filesSettings.BoringWordsFilePath = @"..\..\..\boring words.txt";
        }

        [Test]
        public void GetTagsForVisualization_ReturnTagsWithCorrectFontSize()
        {
            fontSettings.MinFontSize = 1;
            var tagCreator = new TagCreator(fontSettings, new SpiralCloudLayouter(imageSettings),
                new TextAnalyzer(filesSettings), new PictureBoxImageHolder());
            var actual = tagCreator.GetTagsForVisualization().ToArray();
            var expectedFontSizes = new[] {33, 1};
            var expectedWords = new[] {"a", "c"};
            actual.Length.Should().Be(expectedWords.Length);
            for (var i = 0; i < expectedWords.Length; i++)
            {
                actual[i].Font.Size.Should().Be(expectedFontSizes[i]);
                actual[i].Text.Should().Be(expectedWords[i]);
            }
        }

        [Test]
        public void GetTagsForVisualization_NotReturnTagsWithFontSizeSmallerMinFontSize()
        {
            var tagCreator = new TagCreator(fontSettings, new SpiralCloudLayouter(imageSettings),
                new TextAnalyzer(filesSettings), new PictureBoxImageHolder());
            var actual = tagCreator.GetTagsForVisualization().ToArray();
            var expectedFontSizes = new[] {33};
            var expectedWords = new[] {"a"};
            actual.Length.Should().Be(expectedWords.Length);
            for (var i = 0; i < expectedWords.Length; i++)
            {
                actual[i].Font.Size.Should().Be(expectedFontSizes[i]);
                actual[i].Text.Should().Be(expectedWords[i]);
            }
        }
    }
}