using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.ColoringAlgorithms;

namespace TagCloudUI.Infrastructure.Selectors
{
    public class ColoringAlgorithmSelector : IColoringAlgorithmSelector
    {
        private readonly Dictionary<ColoringTheme, IColoringAlgorithm> themeToAlgorithm;

        public ColoringAlgorithmSelector(IEnumerable<IColoringAlgorithm> algorithms)
        {
            themeToAlgorithm = algorithms.ToDictionary(algorithm => algorithm.Theme);
        }

        public bool TryGetAlgorithm(ColoringTheme theme, out IColoringAlgorithm algorithm)
        {
            return themeToAlgorithm.TryGetValue(theme, out algorithm);
        }
    }
}