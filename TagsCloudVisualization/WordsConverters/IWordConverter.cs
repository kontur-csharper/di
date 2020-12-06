using System.Collections.Generic;
using TagsCloudVisualization.CloudTags;

namespace TagsCloudVisualization.WordsConverters
{
    public interface IWordConverter
    {
        public List<ICloudTag> ConvertWords(List<string> words);
    }
}