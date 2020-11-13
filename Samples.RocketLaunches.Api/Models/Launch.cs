using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace Samples.RocketLaunches.Api.Models
{
    public class Launch : BaseModel
    {
        [Required]
        [Name("rocket_company_id")]
        public int CompanyId { get; set; }

        [Required]
        [Name("mission_status_id")]
        public int StatusId { get; set; }

        [Required]
        [Name("launch_location_id")]
        public int LocationId { get; set; }

        [Required]
        [StringLength(250)]
        [Name("mission_name")]
        public string Name { get; set; }

        [Name("mission_cost")]
        public double? Cost { get; set; }

        [Required]
        [Name("launch_time_date")]
        public long LaunchedOnUtc { get; set; }
    }
}
