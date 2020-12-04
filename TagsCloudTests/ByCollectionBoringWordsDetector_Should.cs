using FluentAssertions;
using NUnit.Framework;
using TagsCloud.BoringWordsDetectors;

namespace TagsCloudTests
{
    [TestFixture]
    public class ByCollectionBoringWordsDetector_Should
    {
        private readonly ByCollectionBoringWordsDetector detector = new ByCollectionBoringWordsDetector();
        
        [TestCase("я")]
        [TestCase("ты")]
        [TestCase("вы")]
        [TestCase("он")]
        [TestCase("мы")]
        public void Exclude_RussianPronouns(string word)
        {
            detector.IsBoring(word).Should().BeTrue();
        }
        
        [TestCase("а")]
        [TestCase("и")]
        [TestCase("но")]
        public void Exclude_RussianUnions(string word)
        {
            detector.IsBoring(word).Should().BeTrue();
        }
        
        [TestCase("we")]
        [TestCase("i")]
        [TestCase("you")]
        [TestCase("they")]
        public void Exclude_EnglishPronouns(string word)
        {
            detector.IsBoring(word).Should().BeTrue();
        }
        
        [TestCase("and")]
        [TestCase("but")]
        [TestCase("then")]
        public void Exclude_EnglishUnions(string word)
        {
            detector.IsBoring(word).Should().BeTrue();
        }
    }
}