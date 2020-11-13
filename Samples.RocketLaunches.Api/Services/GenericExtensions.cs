using System.Collections.Generic;
using System.Linq;

namespace Samples.RocketLaunches.Api.Services
{
    public static class GenericExtensions
    {
        public static IReadOnlyList<T> AsListReadOnly<T>(this IEnumerable<T> source)
            => source == null
                   ? null
                   : source is IReadOnlyList<T> rol
                       ? rol
                       : AsList(source).AsReadOnly();

        public static List<T> AsList<T>(this IEnumerable<T> source)
            => source == null
                   ? null
                   : source is List<T> sl
                       ? sl
                       : source.ToList();
    }
}
