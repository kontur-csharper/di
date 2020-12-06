using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.CloudTags;
using TagsCloudVisualization.Configs;

namespace TagsCloudVisualization.WordsConverters
{
    public class WordsToCloudTagConverter : IWordConverter
    {
        private readonly ICloudLayout cloudLayout;
        private readonly IConfig config;

        public WordsToCloudTagConverter(ICloudLayout cloudLayout, IConfig config)
        {
            this.cloudLayout = cloudLayout;
            this.config = config;
        }

        public List<ICloudTag> ConvertWords(List<string> words)
        {
            var result = new List<ICloudTag>();
            var graphics = Graphics.FromHwnd(new IntPtr());
            foreach (var word in words)
            {
                var size = graphics.MeasureString(word, config.Font);
                var rectangle = cloudLayout.PutNextRectangle(size.ToSize());
                var cloudTag = new CloudTag(rectangle, word);
                result.Add(cloudTag);
            }

            return result;
        }
    }
}