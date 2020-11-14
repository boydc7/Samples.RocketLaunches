using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class CachedMetricService : IMetricService
    {
        private readonly ICacheClient _cacheClient;
        private readonly IMetricService _innerMetricService;

        public CachedMetricService(ICacheClient cacheClient, IMetricService innerMetricService)
        {
            _cacheClient = cacheClient;
            _innerMetricService = innerMetricService;
        }

        public QueryMetricsResponse GetMetrics(QueryMetricsRequest request)
        {
            var requestKey = request.ToJson().ToSha256Base64();

            var response = _cacheClient.Get<QueryMetricsResponse>(requestKey);

            if (response != null)
            {
                return response;
            }

            response = _innerMetricService.GetMetrics(request);

            _cacheClient.Set(response, requestKey);

            return response;
        }
    }
}
