using NUnit.Framework;
using TagsCloud.TextProcessing.Converters;

namespace TagsCloudTest
{
    public class ConvertersTests
    {
        private LowerCaseConverter lowerCaseConverter;

        [SetUp]
        public void SetUp()
        {
            lowerCaseConverter = new LowerCaseConverter();
        }

        [TestCase("ABC", ExpectedResult = "abc")]
        [TestCase("AbCdE", ExpectedResult = "abcde")]
        [TestCase("qwerty", ExpectedResult = "qwerty")]
        public string LowerCaseConverter_Should_Convert_To_Lowercase(string word)
        {
            return lowerCaseConverter.Convert(word);
        }
    }
}
