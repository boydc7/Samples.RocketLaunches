using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public interface IMetricService
    {
        QueryMetricsResponse GetMetrics(QueryMetricsRequest request);
    }
}
