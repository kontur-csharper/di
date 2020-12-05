using System;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}