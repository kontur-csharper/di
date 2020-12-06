using System;
using System.Collections.Generic;

namespace WordCloudGenerator
{
    public abstract class AlgorithmFabric
    {
        public static Func<IEnumerable<WordFrequency>, IEnumerable<GraphicString>> Create(AlgorithmType type)
        {
            return type switch
            {
                AlgorithmType.Exponential => GeneratorAlgorithms.Exponential,
                AlgorithmType.Proportional => GeneratorAlgorithms.Proportional,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }

    public enum AlgorithmType
    {
        Exponential = 1,
        Proportional = 2
    }
}