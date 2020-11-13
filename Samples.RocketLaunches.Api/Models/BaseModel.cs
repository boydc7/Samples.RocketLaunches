using System;
using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace Samples.RocketLaunches.Api.Models
{
    public abstract class BaseModel : IHasIntId, IEquatable<BaseModel>
    {
        [Required]
        [Name("id")]
        public int Id { get; set; }

        public bool Equals(BaseModel other)
            => other != null && other.Id == Id;

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is BaseModel bmo && Equals(bmo);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
