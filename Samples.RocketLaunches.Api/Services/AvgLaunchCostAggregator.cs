using System;
using System.Collections.Generic;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class AvgLaunchCostAggregator : IMetricAggregator
    {
        public IEnumerable<Launch> Aggregate(IEnumerable<Launch> launches, QueryMetricsRequest request, QueryMetricsResponse response)
        {
            var costSum = 0d;
            var countWithCost = 0;

            foreach (var launch in launches)
            {
                if (launch.Cost.HasValue)
                {
                    costSum += launch.Cost.Value;
                    countWithCost++;
                }

                yield return launch;
            }

            response.AvgLaunchCost = countWithCost == 0
                                         ? 0
                                         : Math.Round(costSum / countWithCost, 10);
        }
    }
}
