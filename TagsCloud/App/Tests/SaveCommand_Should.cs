using FakeItEasy;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class SaveCommand_Should
    {
        private SaveCommand command;
        private IImageHolder imageHolder;

        [SetUp]
        public void SetUp()
        {
            imageHolder = A.Fake<IImageHolder>();
            command = new SaveCommand(imageHolder);
        }

        [Test]
        public void Execute_CallMethod_SaveImage()
        {
            command.Execute(new[] {"asd"});
            A.CallTo(() => imageHolder.SaveImage("asd")).MustHaveHappened();
        }
    }
}