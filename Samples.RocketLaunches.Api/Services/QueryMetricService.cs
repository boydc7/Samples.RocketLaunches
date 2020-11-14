using System.Collections.Generic;
using System.Linq;
using Samples.RocketLaunches.Api.DataAccess;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class QueryMetricService : IMetricService
    {
        private readonly ILaunchRepository _launchRepository;
        private readonly IReadOnlyList<IMetricAggregator> _metricAggregators;

        public QueryMetricService(ILaunchRepository launchRepository,
                                  IEnumerable<IMetricAggregator> metricAggregators)
        {
            _launchRepository = launchRepository;
            _metricAggregators = metricAggregators.AsListReadOnly();
        }

        public QueryMetricsResponse GetMetrics(QueryMetricsRequest request)
        {
            var startTimestamp = request.StartDate?.ToUnixTimestamp() ?? int.MinValue;
            var endTimestamp = request.EndDate?.ToUnixTimestamp() ?? int.MaxValue;

            var response = new QueryMetricsResponse
                           {
                               CompanyId = request.CompanyId,
                               StartDate = startTimestamp.ToDateTime(),
                               EndDate = endTimestamp.ToDateTime(),
                           };

            // Lazy query the launches, aggregating each result in a pipeline (each aggregator is responsible for calculating and populating
            // the response at the end of evaluation)
            var count = _metricAggregators.Aggregate(_launchRepository.Query(l => l.LaunchedOnUtc >= startTimestamp &&
                                                                                  l.LaunchedOnUtc <= endTimestamp &&
                                                                                  (!request.CompanyId.HasValue || l.CompanyId == request.CompanyId.Value)),
                                                     (current, service) => current.Then(c => service.Aggregate(c, request, response)))
                                          .Count();

            return response;
        }
    }
}
