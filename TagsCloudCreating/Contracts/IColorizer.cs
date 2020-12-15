using System.Drawing;

namespace TagsCloudCreating.Contracts
{
    public interface IColorizer
    {
        public string Name { get; }
        public Color Paint();
    }
}