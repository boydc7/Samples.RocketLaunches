using System.Collections.Generic;

namespace Samples.RocketLaunches.Api.Models
{
    public class RlApiResult<T> : IHasResult<T>
    {
        public T Result { get; set; }
    }

    public class RlApiResults<T> : IHasResults<T>
        where T : class
    {
        public IReadOnlyList<T> Results { get; set; }
        public int ResultCount => Results?.Count ?? 0;
    }
}
