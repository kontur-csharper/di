using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Infrastructure;
using TagsCloud.Layouters;
using TagsCloud.WordsProcessing;

namespace TagsCloud.Tests
{
    class IntegrationTest
    {
        [Test]
        public void CloudBuilderShould_WorkCorrectly()
        {
            var imageWidth = 1920;
            var imageHeight = 1080;
            var container = GetContainerWithSettings(1920, 1080, new HashSet<string> { "ignored", "one", "four" }, "^\\w+$");
            var holder = container.Resolve<IImageHolder>() as PictureBoxImageHolder;
            var fileForTest = CreateFileForTest(new List<string> {"ignored", "one", "one", "two", "two", "two", "three", "four", "five"});

            holder.Image.Width.Should().Be(imageWidth);
            holder.Image.Height.Should().Be(imageHeight);

            holder.Settings.Width = 1000;
            holder.Image.Width.Should().Be(1000);

            holder.Settings.Height = 1000;
            holder.Image.Height.Should().Be(1000);

            holder.RenderWordsFromFile(fileForTest);
            var savedFilePath = Assembly.GetExecutingAssembly().Location + "renderedCloud.png";
            holder.SaveImage(savedFilePath);
            File.Exists(savedFilePath).Should().BeTrue();

            var savedImage = Image.FromFile(savedFilePath);
            savedImage.Width.Should().Be(holder.Settings.Width);
            savedImage.Height.Should().Be(holder.Settings.Height);

            File.Delete(fileForTest);
        }

        private IContainer GetContainerWithSettings(int imageWidth, int imageHeight, HashSet<string> wordsToIgnore, string regexPatternOfWord)
        {
            var builder = new ContainerBuilder();
            builder.Register(_ => new ImageSettings(imageWidth, imageHeight)).As<ImageSettings>().SingleInstance();
            builder.Register(_ => new WordsFilter(wordsToIgnore, regexPatternOfWord)).As<IWordsFilter>().SingleInstance();
            builder.RegisterType<WordsFrequencyParser>().As<IWordsFrequencyParser>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().AsSelf().SingleInstance();
            builder.RegisterType<SpiralCloudLayouter>().As<ICloudLayouter>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder>().SingleInstance();

            return builder.Build();
        }

        private string CreateFileForTest(List<string> words)
        {
            var text = string.Join(Environment.NewLine, words);
            var path = Assembly.GetExecutingAssembly().Location + "testFile.txt";
            File.WriteAllText(path, text);
            return path;
        }
    }
}
