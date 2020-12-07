using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using NUnit.Framework;
using TagCloud.Commands;
using TagCloud.Controllers;
using TagCloud.Extensions;
using TagCloud.Layouters;
using TagCloud.Renderers;
using TagCloud.Settings;
using TagCloud.Sources;
using TagCloud.TagClouds;
using TagCloud.Visualizers;

namespace TagCloud.Tests
{
    [TestFixture]
    public class SourceTests
    {
        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => new SourceSettings
            {
                Destination = "data/example.txt",
                Ignore = new List<string>()
            }).SingleInstance();

            builder.RegisterType<TextSource>().As<ISource>();

            container = builder.Build();
        }

        private IContainer container;

        [Test]
        public void TextSource_ShouldBeIgnoreBoringWords()
        {
            const string filename = "TextSource_ShouldBeIgnoreBoringWords.txt";
            var expected = new[] {"this", "is", "a", "words"};
            container.Resolve<SourceSettings>().Ignore.Add("boring");
            container.Resolve<SourceSettings>().Destination = filename;
            File.WriteAllLines(filename, new[] {"this", "is", "a", "boring", "words"});

            var words = container.Resolve<ISource>().Words().ToArray();

            Assert.AreEqual(expected, words);
        }

        [Test]
        public void TextSource_WordsMustBeInLowercase()
        {
            const string filename = "TextSource_WordsMustBeInLowercase.txt";
            var expected = new[] {"this", "is", "title"};
            container.Resolve<SourceSettings>().Destination = filename;
            File.WriteAllLines(filename, new[] {"This", "Is", "Title"});

            var words = container.Resolve<ISource>().Words().ToArray();

            Assert.AreEqual(expected, words);
        }
    }
}
