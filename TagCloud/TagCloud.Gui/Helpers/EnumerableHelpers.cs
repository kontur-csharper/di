using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Gui.Helpers
{
    internal static class EnumerableHelpers
    {
        public static T MaxOrDefault<T>(this IEnumerable<T> source) where T : IComparable<T> => 
            source.OrderByDescending(x => x, Comparer<T>.Default).FirstOrDefault();
    }
}