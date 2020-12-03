using TagCloud.Core.ColoringAlgorithms;

namespace TagCloudUI.Infrastructure.Selectors
{
    public interface IColoringAlgorithmSelector
    {
        bool TryGetAlgorithm(ColoringTheme theme, out IColoringAlgorithm algorithm);
    }
}