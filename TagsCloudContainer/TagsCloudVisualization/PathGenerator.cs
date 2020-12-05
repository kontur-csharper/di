using System.IO;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class PathGenerator : IPathGenerator
    {
        public PathGenerator(IDateTimeProvider dateTimeProvider)
        {
            Root = Directory.GetCurrentDirectory();
            DateTimeProvider = dateTimeProvider;
        }

        private string Root { get; }
        private IDateTimeProvider DateTimeProvider { get; }

        public string GetNewFilePath()
        {
            var dateTime = DateTimeProvider.GetDateTimeNow();
            return Path.Join(Root, $"{dateTime:MMddyy-HHmmssffffff}.png");
        }
    }
}