using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.Canvases;
using TagsCloudVisualization.PointsGenerators;
using TagsCloudVisualization.TagCloudBuilders;
using TagsCloudVisualization.TagCloudLayouter;
using TagsCloudVisualization.Tags;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualizationTests.TagCloudBuilderTests
{
    public class TagCloudBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            var canvas = new Canvas(new ImageSettings {Width = 100, Height = 100});
            fontSettings = new FontSettings();
            sut = new TagCloudBuilder(
                new CircularCloudLayouter(
                    new ArchimedesSpiral(new SpiralParams(), canvas.GetImageCenter())), fontSettings);
        }

        private ITagCloudBuilder sut;
        private FontSettings fontSettings;

        [Test]
        public void Build_ReturnEmptyList_WhenWordsFreqEmpty()
        {
            var wordsFrequency = new List<Word>();
            
            var result = sut.Build(wordsFrequency);

            result.Should().Equal(new List<Tag>());
        }
        
        [Test]
        public void Build_ReturnLocatedTag_WhenWordsFreqContain1Pair()
        {
            var wordsFrequency = new List<Word> {new Word("hello", 1)};
            
            var result = sut.Build(wordsFrequency);

            result.Count.Should().Be(wordsFrequency.Count);
        }
    }
}