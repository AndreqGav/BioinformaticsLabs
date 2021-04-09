using System.Collections.Generic;
using System.Linq;

namespace BoyerMoore
{
    public static class Extensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static string ToStr(this IEnumerable<char> chars)
        {
            return new string(chars.ToArray());
        }
    }
}