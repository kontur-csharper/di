using System;
using System.IO;
using FakeItEasy;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class HelpCommand_Should
    {
        private HelpCommand command;
        private TextWriter writer;

        [SetUp]
        public void SetUp()
        {
            writer = A.Fake<TextWriter>();
            command = new HelpCommand(new Lazy<IClient>(A.Fake<IClient>()), writer);
        }

        [Test]
        public void Execute_CallMethod_WriteLine()
        {
            command.Execute(new string[0]);
            A.CallTo(() => writer.WriteLine(A<string>.Ignored)).MustHaveHappened();
        }
    }
}