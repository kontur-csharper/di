using System.Collections.Generic;

namespace TagCloud.PointGetters
{
    public static class PointGetterAssosiation
    {
        public const string circle = "circle";
        public const string spiral = "spiral";
        private static readonly Dictionary<string, IPointGetter> pointGetters =
            new Dictionary<string, IPointGetter>
            {
                [circle] = new CirclePointGetter(),
                [spiral] = new SpiralPointGetter()
            };

        public static IPointGetter GetPointGetter(string name)
        {
            return pointGetters.TryGetValue(name, out var getter) ? getter : null;
        }
    }
}
