using System.Collections.Generic;

namespace TagsCloudContainer.TagsCloudContainer.Interfaces
{
    public interface ITextParser
    {
        public List<string> GetAllWords(string text);
    }
}