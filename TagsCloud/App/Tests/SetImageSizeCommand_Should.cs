using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class SetImageSizeCommand_Should
    {
        private SetImageSizeCommand command;
        private IImageSizeProvider imageSizeProvider;

        [SetUp]
        public void SetUp()
        {
            imageSizeProvider = A.Fake<IImageSizeProvider>();
            command = new SetImageSizeCommand(imageSizeProvider);
        }

        [Test]
        public void Execute_Set_ImageSize()
        {
            var newImageSize = new ImageSize
            {
                Height = 150,
                Width = 150
            };
            command.Execute(new[] {"150", "150"});
            imageSizeProvider.ImageSize.Height.Should().Be(newImageSize.Height);
            imageSizeProvider.ImageSize.Width.Should().Be(newImageSize.Width);
        }

        [TestCase("asd", "12", ExpectedResult = false, TestName = "when input is not number")]
        [TestCase("0", "12", ExpectedResult = false, TestName = "when width is zero")]
        [TestCase("12", "0", ExpectedResult = false, TestName = "when height is zero")]
        [TestCase("-5", "12", ExpectedResult = false, TestName = "when width is negative")]
        [TestCase("12", "-5", ExpectedResult = false, TestName = "when height is negative")]
        [TestCase("12", "13", ExpectedResult = true, TestName = "when arguments is correct")]
        public bool Execute_ReturnsCorrectResult(string width, string height) =>
            command.Execute(new[] {width, height}).IsSuccess;
    }
}