using System.Collections.Generic;

namespace Samples.RocketLaunches.Api.Models
{
    public interface IHasResult<out T>
    {
        T Result { get; }
    }

    public interface IHasResults<out T>
        where T : class
    {
        IReadOnlyList<T> Results { get; }
    }
}
