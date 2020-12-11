using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App.Commands;
using TagsCloud.App.FileReaders;
using TagsCloud.App.ImageSavers;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class TagsCloudTests
    {
        private AddColorCommand addColorCommand;
        private SaveCommand saveCommand;
        private DirectoryInfo savingDirectory;
        private SetFontCommand setFontCommand;
        private SetImageSizeCommand setImageSizeCommand;
        private DirectoryInfo sourceDataDirectory;
        private TagCloudCommand tagCloudCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            sourceDataDirectory = new DirectoryInfo(Path.Combine("..", "..", "..", "Test_Data"));
            savingDirectory = new DirectoryInfo(Path.Combine("..", "..", "..", "Tag_Clouds_From_Tests"));
            ClearSavingDirectory();
        }

        [SetUp]
        public void SetUp()
        {
            var imageSavers = new List<IImageSaver> {new PngSaver(), new JpgSaver()};
            var fileReaders = new List<IFileAllLinesReader> {new TxtReader(), new DocReader()};
            var fileReader = new FileReader(new FileReaderProvider(fileReaders));
            var imageHolder = new ImageHolder(new ImageSaverProvider(imageSavers));
            var imageSettings = new ImageSettings();
            var wordFrequency = new WordFrequency(new GrammemeChecker());
            var painter = new TagCloudPainter(imageHolder, imageSettings);
            var algorithmGenerator = new Func<Point, ILayoutAlgorithm>(x => new SpiralAlgorithm(x));
            var layoutGenerator = new Func<ILayoutAlgorithm, TagCloudLayouter>(x => new TagCloudLayouter(x));
            tagCloudCommand = new TagCloudCommand(
                painter, wordFrequency, imageSettings, imageSettings, algorithmGenerator, layoutGenerator, fileReader);
            saveCommand = new SaveCommand(imageHolder);
            addColorCommand = new AddColorCommand(imageSettings);
            setFontCommand = new SetFontCommand(imageSettings);
            setImageSizeCommand = new SetImageSizeCommand(imageSettings);
        }

        [Test]
        public void ShouldMakeTagCloud_IfSourceFileIsTxt()
        {
            tagCloudCommand.Execute(new[] {GetPathToTestFile("txt_file.txt")});
            saveCommand.Execute(new[] {GetPathForSavingTagCloud("txt_file.png")});
            savingDirectory.GetFiles().Select(x => x.Name).Should().Contain("txt_file.png");
        }

        [Test]
        public void ShouldMakeTagCloud_IfSourceFileIsDoc()
        {
            tagCloudCommand.Execute(new[] {GetPathToTestFile("doc_file.doc")});
            saveCommand.Execute(new[] {GetPathForSavingTagCloud("doc_file.png")});
            savingDirectory.GetFiles().Select(x => x.Name).Should().Contain("doc_file.png");
        }

        [Test]
        public void ShouldMakeTagCloud_IfSourceFileHasMultipleWords()
        {
            tagCloudCommand.Execute(new[] {GetPathToTestFile("multiple_words.txt")});
            saveCommand.Execute(new[] {GetPathForSavingTagCloud("multiple_words.png")});
            savingDirectory.GetFiles().Select(x => x.Name).Should().Contain("multiple_words.png");
        }

        [Test]
        public void ShouldMakeTagCloud_IfDestinationFileIsJpg()
        {
            tagCloudCommand.Execute(new[] {GetPathToTestFile("txt_file.txt")});
            saveCommand.Execute(new[] {GetPathForSavingTagCloud("txt_file.jpg")});
            savingDirectory.GetFiles().Select(x => x.Name).Should().Contain("txt_file.jpg");
        }

        [Test]
        public void ShouldMakeTagCloud_IfColorSet()
        {
            addColorCommand.Execute(new[] {"Blue"});
            tagCloudCommand.Execute(new[] {GetPathToTestFile("txt_file.txt")});
            saveCommand.Execute(new[] {GetPathForSavingTagCloud("single_color.jpg")});
            savingDirectory.GetFiles().Select(x => x.Name).Should().Contain("single_color.jpg");
        }

        [Test]
        public void ShouldMakeTagCloud_IfFontSet()
        {
            setFontCommand.Execute(new[] {"Georgia"});
            tagCloudCommand.Execute(new[] {GetPathToTestFile("txt_file.txt")});
            saveCommand.Execute(new[] {GetPathForSavingTagCloud("not_default_font.jpg")});
            savingDirectory.GetFiles().Select(x => x.Name).Should().Contain("not_default_font.jpg");
        }

        [Test]
        public void ShouldMakeTagCloud_IfImageSizeSet()
        {
            setImageSizeCommand.Execute(new[] {"250", "250"});
            tagCloudCommand.Execute(new[] {GetPathToTestFile("txt_file.txt")});
            saveCommand.Execute(new[] {GetPathForSavingTagCloud("not_default_size.jpg")});
            savingDirectory.GetFiles().Select(x => x.Name).Should().Contain("not_default_size.jpg");
        }

        private string GetPathToTestFile(string fileName) => Path.Combine(sourceDataDirectory.FullName, fileName);

        private string GetPathForSavingTagCloud(string fileName) => Path.Combine(savingDirectory.FullName, fileName);

        private void ClearSavingDirectory()
        {
            foreach (var file in savingDirectory.GetFiles())
            {
                file.Delete();
            }
        }
    }
}