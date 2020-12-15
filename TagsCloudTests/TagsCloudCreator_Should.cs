using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using TagsCloudCreating.Configuration;
using TagsCloudCreating.Core;
using TagsCloudCreating.Core.CircularCloudLayouter;
using TagsCloudCreating.Core.WordProcessors;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudTests
{
    [UseReporter(typeof(DiffReporter), typeof(FileLauncherReporter))]
    [TestFixture]
    public class TagsCloudCreator_Should
    {
        [Test]
        public void CreateTagsCLoud_FileForTest_ReturnsCorrectCloudTags()
        {
            var tagsCloudCreator = new TagsCloudCreator(
                new CircularCloudLayouter(new CloudLayouterSettings()),
                new WordHandler(new WordHandlerSettings()),
                new WordConverter(new TagsSettings())
            );

            var tags = tagsCloudCreator.CreateTagsCloud(new FileWordsReader().GetAllData("FileForTest.txt"));
            Approvals.VerifyAll(
                tags,
                tag =>
                    $" Tag{{\"{tag.Word}\" {nameof(tag.Frequency)}={tag.Frequency} {nameof(tag.Frame)}={tag.Frame}}}{Environment.NewLine}"
            );
        }
    }
}