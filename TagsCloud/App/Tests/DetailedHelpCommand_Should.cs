using System;
using System.IO;
using FakeItEasy;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class DetailedHelpCommand_Should
    {
        private IClient client;
        private DetailedHelpCommand command;
        private TextWriter writer;

        [SetUp]
        public void SetUp()
        {
            client = A.Fake<IClient>();
            writer = A.Fake<TextWriter>();
            command = new DetailedHelpCommand(new Lazy<IClient>(client), writer);
            A.CallTo(() => client.FindCommandByName("help")).Returns(command);
        }

        [Test]
        public void Execute_CallMethod_FindCommandByName()
        {
            command.Execute(new[] {"asd"});
            A.CallTo(() => client.FindCommandByName(A<string>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Execute_CallMethod_WriteLine()
        {
            command.Execute(new[] {"help"});
            A.CallTo(() => writer.WriteLine(A<string>.Ignored)).MustHaveHappened();
        }
        
        [TestCase("help", ExpectedResult = true, TestName = "on known command")]
        [TestCase("asd", ExpectedResult = false, TestName = "on unknown command")]
        public bool Execute_ReturnsCorrectResult(string cmdForHelp) => command.Execute(new[] {cmdForHelp}).IsSuccess;
    }
}