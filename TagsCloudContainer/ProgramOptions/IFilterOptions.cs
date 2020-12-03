using System.Collections.Generic;

namespace TagsCloudContainer.ProgramOptions
{
    public interface IFilterOptions
    {
        public string MystemLocation { get; set; }

        public IEnumerable<string> BoringWords { get; set; }
    }
}