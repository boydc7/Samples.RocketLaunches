using System.Collections.Generic;
using System.Linq;
using Samples.RocketLaunches.Api.DataAccess;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class TopLocationsAggregator : IMetricAggregator
    {
        private readonly ILocationRepository _locationRepository;

        public TopLocationsAggregator(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public IEnumerable<Launch> Aggregate(IEnumerable<Launch> launches, QueryMetricsRequest request, QueryMetricsResponse response)
        {
            var locationCountMap = new Dictionary<int, int>();

            foreach (var launch in launches)
            {
                if (!locationCountMap.ContainsKey(launch.LocationId))
                {
                    locationCountMap[launch.LocationId] = 0;
                }

                locationCountMap[launch.LocationId]++;

                yield return launch;
            }

            if (locationCountMap.Count > 0)
            {
                response.TopLocations = locationCountMap.OrderByDescending(m => m.Value)
                                                        .Take(request.TopLocationsCount)
                                                        .Select(t => _locationRepository.GetById(t.Key))
                                                        .AsListReadOnly();
            }
        }
    }
}
