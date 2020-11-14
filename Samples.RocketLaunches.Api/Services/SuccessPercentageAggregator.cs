using System;
using System.Collections.Generic;
using System.Linq;
using Samples.RocketLaunches.Api.DataAccess;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class SuccessPercentageAggregator : IMetricAggregator
    {
        private readonly int _successStatusId;

        public SuccessPercentageAggregator(IStatusRepository statusRepository)
        {
            _successStatusId = statusRepository.Query(s => s.Name.Equals("success", StringComparison.OrdinalIgnoreCase))
                                               .Single()
                                               .Id;
        }

        public IEnumerable<Launch> Aggregate(IEnumerable<Launch> launches, QueryMetricsRequest request, QueryMetricsResponse response)
        {
            var totalCount = 0;
            var successCount = 0;

            foreach (var launch in launches)
            {
                if (launch.StatusId == _successStatusId)
                {
                    successCount++;
                }

                totalCount++;

                yield return launch;
            }

            response.PctMissionSuccess = totalCount == 0
                                             ? 0
                                             : Math.Round((successCount / (double)totalCount) * 100.0, 5);
        }
    }
}
