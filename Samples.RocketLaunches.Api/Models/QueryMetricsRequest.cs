using System;

namespace Samples.RocketLaunches.Api.Models
{
    public class QueryMetricsRequest
    {
        public int? CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
