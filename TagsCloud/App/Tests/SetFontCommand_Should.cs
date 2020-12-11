using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class SetFontCommand_Should
    {
        private SetFontCommand command;
        private IFontFamilyProvider fontFamilyProvider;

        [SetUp]
        public void SetUp()
        {
            fontFamilyProvider = A.Fake<IFontFamilyProvider>();
            command = new SetFontCommand(fontFamilyProvider);
        }

        [Test]
        public void Execute_Set_FamilyFont()
        {
            var fontName = "Georgia";
            command.Execute(new[] {fontName});
            fontFamilyProvider.FontFamily.Should().Be(new FontFamily(fontName));
        }

        [TestCase("Georgia", ExpectedResult = true, TestName = "on known font")]
        [TestCase("asd", ExpectedResult = false, TestName = "on unknown font")]
        public bool Execute_ReturnsCorrectResult(string font) => command.Execute(new[] {font}).IsSuccess;
    }
}