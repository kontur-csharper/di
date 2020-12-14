using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.Factory;
using TagsCloudVisualization;

namespace TagCloudTests
{
    [TestFixture]
    public class WordsForCloudGenerator_Should
    {
        private WordsForCloudGenerator wordsForCloudGenerator;
        private const string FontName = "Arial";
        private const int MaxFontSize = 10;
        private static readonly Point Center = new Point(500, 500);
        private Color[] colors = new[] {Color.Black};
        private TagCloudCreatorFactory tagCloudCreatorFactory;

        [SetUp]
        public void Setup()
        {
            Directory.SetCurrentDirectory(
                Directory.GetParent(
                    Directory.GetParent(
                        TestContext.CurrentContext.TestDirectory).ToString()) + "\\testFiles");
            Directory.GetParent(TestContext.CurrentContext.TestDirectory);

            tagCloudCreatorFactory = new TagCloudCreatorFactory(new WordsForCloudGeneratorFactory(),
                                                                new ColorGeneratorFactory(),
                                                                new CloudDrawerFactory(),
                                                                new TagCloudLayouterFactory(),
                                                                new SpiralPointsFactory(),
                                                                new WordsReader(),
                                                                new WordsNormalizer());

            wordsForCloudGenerator = new WordsForCloudGenerator(FontName,
                                                                MaxFontSize,
                                                                new CircularCloudLayouter(new SpiralPoints(Center)),
                                                                new ColorGenerator(colors));
        }

        [Test]
        public void NotRepeated_OnRepeatedWords()
        {
            wordsForCloudGenerator.Generate(new List<string> {"w", "w", "e"}).Count.Should().Be(2);
        }

        [Test]
        public void MostCommonWord_OnFirstPlace()
        {
            wordsForCloudGenerator.Generate(new List<string> {"w", "w", "e", "e", "e", "c"})[0].Word.Should().Be("e");
        }

        [Test]
        public void FirstWord_OnCenter()
        {
            var wordForCloud = wordsForCloudGenerator.Generate(new List<string> {"w", "w", "e", "e", "e", "c"})[0];
            wordForCloud.WordSize.Location.Should()
                        .Be(new Point(Center.X - wordForCloud.WordSize.Width / 2,
                                      Center.Y - wordForCloud.WordSize.Height / 2));
        }

        [Test]
        public void HaveDescendingOrder()
        {
            var wordsForCloud = wordsForCloudGenerator.Generate(new List<string> {"e", "w", "w"});
            wordsForCloud[0].Font.Size.Should().BeGreaterThan(wordsForCloud[1].Font.Size);
        }

        [Test]
        public void PictureCreation_WithBoringWords()
        {
            var tagCloudCreatorWithBoringWords = tagCloudCreatorFactory
                .Get(new Size(2000, 2000),
                     new Point(1000, 1000),
                     new[] {Color.Black, Color.Blue,},
                     "Arial",
                     50,
                     "in.txt",
                     null);
            tagCloudCreatorWithBoringWords.GetCloud().Save("WithBoringWords.bmp");
            File.Exists("WithBoringWords.bmp").Should().BeTrue();
        }

        [Test]
        public void PictureCreation_WithoutBoringWords()
        {
            var tagCloudCreatorWithoutBoringWords = tagCloudCreatorFactory
                .Get(new Size(2000, 2000),
                     new Point(1000, 1000),
                     new[] {Color.Black, Color.Blue,},
                     "Arial",
                     50,
                     "in.txt",
                     "boringWords.txt");
            tagCloudCreatorWithoutBoringWords.GetCloud().Save("WithoutBoringWords.bmp");
            File.Exists("WithoutBoringWords.bmp").Should().BeTrue();
        }
    }
}