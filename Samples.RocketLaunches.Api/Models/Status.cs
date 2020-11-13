using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace Samples.RocketLaunches.Api.Models
{
    public class Status : BaseModel
    {
        [Required]
        [StringLength(50)]
        [Name("status")]
        public string Name { get; set; }
    }
}
