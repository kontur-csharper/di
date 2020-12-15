using System.Collections.Generic;
using TagCloud.PointGetters;

namespace TagCloud.CloudLayoters
{
    public static class CloudLayoterAssosiation
    {
        public const string density = "density";
        public const string identity = "identity";
        private static readonly Dictionary<string, ICloudLayoter> cloudLayoters =
            new Dictionary<string, ICloudLayoter>
            {
                [density] = new DensityCloudLayouter(),
                [identity] = new IdentityCloudLayoter()
            };

        public static ICloudLayoter GetCloudLayoter(string nameLayoter, string namePointGetter)
        {
            var pointGetter = PointGetterAssosiation.GetPointGetter(namePointGetter);
            if (pointGetter == null)
                return null;
            if (!cloudLayoters.TryGetValue(nameLayoter, out var layoter))
                return null;
            layoter.SetPointGetterIfNull(pointGetter);
            return layoter;
        } 
    }
}
