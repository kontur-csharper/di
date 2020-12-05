using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class TxtFileWordsLoaderTests
    {
        [Test]
        public void Ctor_IfFileFromPathDoesNotExist_ThrowArgumentException()
        {
            Action act = () => new TxtFileWordsLoader("doesn't exist");
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Ctor_IfFileExtensionIsNotSupported_ThrowArgumentException()
        {
            Action act = () => new TxtFileWordsLoader("../../../TestFiles/test.rtf");
            act.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void GetWords_CorrectWorkWithTxt()
        {
            var loader = new TxtFileWordsLoader("../../../TestFiles/test.txt");

            loader.GetWords().Should().BeEquivalentTo(
                new[] {"1", "2", "3"},
                options => options.WithStrictOrdering());
        }
    }
}