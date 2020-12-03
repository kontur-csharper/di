using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.LayoutAlgorithms;

namespace TagCloudUI.Infrastructure.Selectors
{
    public class LayoutAlgorithmSelector : ILayoutAlgorithmSelector
    {
        private readonly Dictionary<LayoutAlgorithmType, ILayoutAlgorithm> typeToAlgorithm;

        public LayoutAlgorithmSelector(IEnumerable<ILayoutAlgorithm> algorithms)
        {
            typeToAlgorithm = algorithms.ToDictionary(algorithm => algorithm.Type);
        }

        public bool TryGetAlgorithm(LayoutAlgorithmType type, out ILayoutAlgorithm algorithm)
        {
            return typeToAlgorithm.TryGetValue(type, out algorithm);
        }
    }
}