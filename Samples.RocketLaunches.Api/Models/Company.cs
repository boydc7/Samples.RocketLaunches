using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace Samples.RocketLaunches.Api.Models
{
    public class Company : BaseModel
    {
        [Required]
        [StringLength(100)]
        [Name("name")]
        public string Name { get; set; }
    }
}
