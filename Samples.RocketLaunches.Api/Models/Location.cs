using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace Samples.RocketLaunches.Api.Models
{
    public class Location : BaseModel
    {
        [Required]
        [StringLength(250)]
        [Name("location")]
        public string Name { get; set; }
    }
}
