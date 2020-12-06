using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsProviders;

namespace TagsCloudVisualization_Should
{
    public class TxtWordProviderShould
    {
        [Test]
        public void GetWords_ListWithWords_TxtDocument()
        {
            var provider = new TxtWordProvider();
            var path = Path.Join(Directory.GetCurrentDirectory(), "text.txt");
            var expectedWords = new List<string> {"он", "пошел"};

            var actualWords = provider.GetWords(path);

            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}