using System.Collections.Generic;
using System.Linq;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class TopYearAggregator : IMetricAggregator
    {
        public IEnumerable<Launch> Aggregate(IEnumerable<Launch> launches, QueryMetricsRequest request, QueryMetricsResponse response)
        {
            var yearCountMap = new Dictionary<int, int>();

            foreach (var launch in launches)
            {
                var launchDate = launch.LaunchedOnUtc.ToDateTime();

                if (!yearCountMap.ContainsKey(launchDate.Year))
                {
                    yearCountMap[launchDate.Year] = 0;
                }

                yearCountMap[launchDate.Year]++;

                yield return launch;
            }

            if (yearCountMap.Count > 0)
            {
                response.TopYearByLaunchCount = yearCountMap.OrderByDescending(v => v.Value)
                                                            .First()
                                                            .Key;
            }
        }
    }
}
