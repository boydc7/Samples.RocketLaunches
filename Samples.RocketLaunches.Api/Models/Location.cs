using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace Samples.RocketLaunches.Api.Models
{
    public class Location : BaseModel
    {
        [Required]
        [StringLength(250)]
        public string Platform { get; set; }

        [StringLength(250)]
        public string SiteName { get; set; }

        [StringLength(250)]
        public string RegionName { get; set; }

        [StringLength(250)]
        public string CountryName { get; set; }
    }

    public class CsvLocation
    {
        [Name("id")]
        public int Id { get; set; }

        [Name("location")]
        public string Location { get; set; }
    }
}
