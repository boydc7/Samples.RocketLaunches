using System.Collections.Generic;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public interface IMetricAggregator
    {
        IEnumerable<Launch> Aggregate(IEnumerable<Launch> launches, QueryMetricsRequest request, QueryMetricsResponse response);
    }
}
