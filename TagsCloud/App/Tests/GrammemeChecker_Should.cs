using NUnit.Framework;

namespace TagsCloud.App.Tests
{
    public class GrammemeChecker_Should
    {
        private GrammemeChecker grammemeChecker;

        [SetUp]
        public void SetUp() => grammemeChecker = new GrammemeChecker();

        [TestCase("говорить", ExpectedResult = true, TestName = "if word is verb")]
        [TestCase("стол", ExpectedResult = true, TestName = "if word is noun")]
        [TestCase("навсегда", ExpectedResult = true, TestName = "if word is adverb")]
        [TestCase("красивый", ExpectedResult = true, TestName = "if word is adjective")]
        [TestCase("пять", ExpectedResult = true, TestName = "if word is numeral")]
        [TestCase("десятый", ExpectedResult = true, TestName = "if word is numeral-adjective")]
        [TestCase("и", ExpectedResult = false, TestName = "if word is conjunction")]
        [TestCase("не", ExpectedResult = false, TestName = "if word is particle")]
        [TestCase("я", ExpectedResult = false, TestName = "if word is pronoun")]
        [TestCase("ах", ExpectedResult = false, TestName = "if word is interjection")]
        [TestCase("нигде", ExpectedResult = false, TestName = "if word is pronominal adverb")]
        [TestCase("тот", ExpectedResult = false, TestName = "if word is pronoun-adjective")]
        [TestCase("в", ExpectedResult = false, TestName = "if word is preposition ")]
        public bool ReturnsCorrectValue(string word) => grammemeChecker.IsWordNotBoring(word);
    }
}