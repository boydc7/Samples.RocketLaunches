using System.Collections.Generic;
using System.Linq;
using Samples.RocketLaunches.Api.DataAccess;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class TopCountriesAggregator : IMetricAggregator
    {
        private readonly ILocationRepository _locationRepository;

        public TopCountriesAggregator(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public IEnumerable<Launch> Aggregate(IEnumerable<Launch> launches, QueryMetricsRequest request, QueryMetricsResponse response)
        {
            var countryCountMap = new Dictionary<string, int>();

            foreach (var launch in launches)
            {
                // For something like this I'd typically wrap the location repo access here with an in-memory cache of the locationIds for use
                // inside a tight loop (or even request scoped), but for demo purposes, not doing so....
                var location = _locationRepository.GetById(launch.LocationId);
                var countryName = location.CountryName ?? "";

                if (!countryCountMap.ContainsKey(countryName))
                {
                    countryCountMap[countryName] = 0;
                }

                countryCountMap[countryName]++;

                yield return launch;
            }

            if (countryCountMap.Count > 0)
            {
                response.TopCountries = countryCountMap.OrderByDescending(m => m.Value)
                                                       .Take(request.TopCountriesCount)
                                                       .Select(t => t.Key)
                                                       .AsListReadOnly();
            }
        }
    }
}
