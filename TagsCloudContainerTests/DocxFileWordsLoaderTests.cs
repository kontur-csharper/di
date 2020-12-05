using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class DocxFileWordsLoaderTests
    {
        [Test]
        public void Ctor_IfFileFromPathDoesNotExist_ThrowArgumentException()
        {
            Action act = () => new DocxFileWordsLoader("doesn't exist");
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Ctor_IfFileExtensionIsNotSupported_ThrowArgumentException()
        {
            Action act = () => new DocxFileWordsLoader("../../../TestFiles/test.rtf");
            act.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void GetWords_CorrectWorkWithDocx()
        {
            var loader = new DocxFileWordsLoader("../../../TestFiles/test.docx");

            loader.GetWords().Should().BeEquivalentTo(
                new[] {"1", "2", "3"},
                options => options.WithStrictOrdering());
        }
    }
}