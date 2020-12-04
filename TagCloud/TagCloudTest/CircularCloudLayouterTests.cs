using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud;
using TagCloud.Curves;
using TagCloud.WordsFilter;
using TagCloud.WordsProvider;

namespace TagCloudTest
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            spiral = new ArchimedeanSpiral(new Point(0, 0));
            wordsProvider = new CircularWordsProvider();
            wordsFilter = new WordsFilter().Normalize();
            tagCloudWithCenterInZero =
                new CircularCloudLayouter(spiral, wordsProvider, wordsFilter);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;
            var fileName = $"{TestContext.CurrentContext.Test.Name}_Failed.jpg";
            var path = $"../../../FailedTests/{fileName}";
            var visualizer = new TagCloudVisualizer(tagCloudWithCenterInZero);
            var image = visualizer.CreateBitMap(1920, 1080,
                new[] {Color.Blue, Color.Aqua},
                "Times New Roman");
            image.Save(path);
        }

        private ITagCloud tagCloudWithCenterInZero;
        private ICurve spiral;
        private readonly Random rnd = new Random();
        private IWordsProvider wordsProvider;
        private IWordsFilter wordsFilter;

        private Size GetRandomSize()
        {
            return new Size(rnd.Next() % 100 + 1, rnd.Next() % 100 + 1);
        }

        [Test]
        public void PutNextRectangle_DoesntThrow()
        {
            Assert.DoesNotThrow(() => tagCloudWithCenterInZero.PutNextWord("abc", new Size(10, 10)));
        }

        [Test]
        public void PutNextRectangle_PutsAllRectangles()
        {
            var expectedRectanglesCount = 15;
            for (var i = 0; i < expectedRectanglesCount; i++)
                tagCloudWithCenterInZero.PutNextWord("abc", GetRandomSize());

            tagCloudWithCenterInZero.WordRectangles.Should().HaveCount(expectedRectanglesCount);
        }

        [Test]
        public void PutNextRectangle_PutsFirstRectangleInCenter()
        {
            var center = new Point(10, 18);
            var shiftedTagCloud = new CircularCloudLayouter(new ArchimedeanSpiral(center), wordsProvider, wordsFilter);
            shiftedTagCloud.PutNextWord("dsadsa", new Size(10, 5));

            shiftedTagCloud.WordRectangles[0].Rectangle.Location.Should().Be(center);
        }

        [Test]
        public void Rectangles_ShouldNotIntersect()
        {
            for (var i = 0; i < 100; i++)
                tagCloudWithCenterInZero.PutNextWord("dadas", GetRandomSize());

            foreach (var wordRectangle in tagCloudWithCenterInZero.WordRectangles)
                tagCloudWithCenterInZero.WordRectangles.All(
                        other => other.Equals(wordRectangle)
                                 || !other.Rectangle.IntersectsWith(wordRectangle.Rectangle))
                    .Should().BeTrue();
        }

        [Test]
        public void TagCloud_ShouldContainUniqueWords()
        {
            tagCloudWithCenterInZero.GenerateTagCloud();

            tagCloudWithCenterInZero.WordRectangles.Select(wordRectangle => wordRectangle.Word).Should()
                .BeEquivalentTo(wordsFilter.Apply(wordsProvider.GetWords().ToHashSet()));
        }

        [Test]
        [Timeout(200)]
        public void Put1000Rectangles_StopsInSufficientTime()
        {
            for (var i = 0; i < 1000; i++)
                tagCloudWithCenterInZero.PutNextWord("asda", GetRandomSize());
        }
    }
}