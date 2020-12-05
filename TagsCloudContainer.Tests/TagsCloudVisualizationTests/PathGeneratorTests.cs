using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests.TagsCloudVisualizationTests
{
    public class PathGeneratorTests
    {
        private PathGenerator Generator { get; set; }

        [SetUp]
        public void SetUp()
        {
            Generator = new PathGenerator(new DateTimeProvider());
        }

        [Test]
        public void GetNewFilePath_ReturnExistingDirectory_WhenCalled()
        {
            var path = Generator.GetNewFilePath().Split(Path.DirectorySeparatorChar);
            path[^1] = "";
            var directoryPath = string.Join(Path.DirectorySeparatorChar, path);

            Directory.Exists(directoryPath).Should().BeTrue();
        }
    }
}