using System;
using System.Collections.Generic;

namespace Samples.RocketLaunches.Api.Models
{
    public class QueryMetricsResponse
    {
        public int? CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public double AvgLaunchCost { get; set; }
        public double PctMissionSuccess { get; set; }
        public string TopMonthByLaunchCount { get; set; }
        public int TopYearByLaunchCount { get; set; }
        public IReadOnlyList<Location> TopLocations { get; set; }
        public IReadOnlyList<string> TopCountries { get; set; }
    }
}
