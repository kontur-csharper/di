using TagCloud.Core.LayoutAlgorithms;

namespace TagCloudUI.Infrastructure.Selectors
{
    public interface ILayoutAlgorithmSelector
    {
        bool TryGetAlgorithm(LayoutAlgorithmType type, out ILayoutAlgorithm algorithm);
    }
}