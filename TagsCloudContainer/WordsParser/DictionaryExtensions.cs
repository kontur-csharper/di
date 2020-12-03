using System.Collections.Generic;

namespace TagsCloudContainer.WordsParser
{
    public static class DictionaryExtensions
    {
        public static void SetOrUpdate(this Dictionary<string, int> dict, string key)
        {
            if (dict.ContainsKey(key))
                dict[key] += 1;
            else
                dict[key] = 1;
        }
    }
}