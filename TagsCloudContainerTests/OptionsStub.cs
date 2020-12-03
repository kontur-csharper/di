using System.Collections.Generic;
using TagsCloudContainer.ProgramOptions;

namespace TagsCloudContainerTests
{
    internal class OptionsStub: IFilterOptions, IImageOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageOutputDirectory { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public string MystemLocation { get; set; }
        public IEnumerable<string> BoringWords { get; set; }

        public OptionsStub(int width, int height)
        {
            Width = width;
            Height = height;
            BoringWords = new List<string>();
        }
    }
}