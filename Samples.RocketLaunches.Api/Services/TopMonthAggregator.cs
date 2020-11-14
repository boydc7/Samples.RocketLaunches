using System.Collections.Generic;
using System.Linq;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class TopMonthAggregator : IMetricAggregator
    {
        private static readonly Dictionary<int, string> _monthNameMap = new Dictionary<int, string>
                                                                        {
                                                                            {
                                                                                1, "January"
                                                                            },
                                                                            {
                                                                                2, "February"
                                                                            },
                                                                            {
                                                                                3, "March"
                                                                            },
                                                                            {
                                                                                4, "April"
                                                                            },
                                                                            {
                                                                                5, "May"
                                                                            },
                                                                            {
                                                                                6, "June"
                                                                            },
                                                                            {
                                                                                7, "July"
                                                                            },
                                                                            {
                                                                                8, "August"
                                                                            },
                                                                            {
                                                                                9, "September"
                                                                            },
                                                                            {
                                                                                10, "October"
                                                                            },
                                                                            {
                                                                                11, "November"
                                                                            },
                                                                            {
                                                                                12, "December"
                                                                            },
                                                                        };

        public IEnumerable<Launch> Aggregate(IEnumerable<Launch> launches, QueryMetricsRequest request, QueryMetricsResponse response)
        {
            var monthCountMap = new Dictionary<int, int>();

            foreach (var launch in launches)
            {
                var launchDate = launch.LaunchedOnUtc.ToDateTime();

                if (!monthCountMap.ContainsKey(launchDate.Month))
                {
                    monthCountMap[launchDate.Month] = 0;
                }

                monthCountMap[launchDate.Month]++;

                yield return launch;
            }

            if (monthCountMap.Count > 0)
            {
                response.TopMonthByLaunchCount = _monthNameMap[monthCountMap.OrderByDescending(m => m.Value)
                                                                            .First()
                                                                            .Key];
            }
        }
    }
}
