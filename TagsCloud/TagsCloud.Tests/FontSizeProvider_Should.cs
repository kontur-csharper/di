using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.WordsProcessing;

namespace TagsCloud.Tests
{
    class FontSizeProvider_Should
    {
        [TestCase(10, 0.5, 15)]
        [TestCase(3, 0.333, 4)]
        [TestCase(10, 0.2, 12)]
        public void ProvideSizeCorrectly(float defaultSize, double wordFrequency, float expectedSize)
        {
            FontSizeProvider.GetFontSize(defaultSize, wordFrequency)
                .Should().BeApproximately(expectedSize, 0.01f);
        }
    }
}
