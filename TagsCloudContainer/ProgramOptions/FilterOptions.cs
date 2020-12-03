using System.Collections.Generic;

namespace TagsCloudContainer.ProgramOptions
{
    public class FilterOptions : IFilterOptions
    {
        public string MystemLocation { get; set; }
        public IEnumerable<string> BoringWords { get; set; }

        public FilterOptions(string mystemLocation, IEnumerable<string> boringWords)
        {
            MystemLocation = mystemLocation;
            BoringWords = boringWords;
        }
    }
}