using System;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class StaticCsvLocationTransformer : ITransformer<CsvLocation, Location>
    {
        public Location To(CsvLocation source)
        {
            if (string.IsNullOrEmpty(source?.Location))
            {
                throw new ArgumentOutOfRangeException(nameof(CsvLocation.Location));
            }

            // Split the location on commas
            var splits = source.Location.Split(',', StringSplitOptions.RemoveEmptyEntries);

            // First component is always the platform name
            var location = new Location
                           {
                               Id = source.Id,
                               Platform = splits[0]
                           };

            if (splits.Length <= 1)
            {
                return location;
            }

            switch (splits.Length)
            {
                case 2:
                {
                    // If only 2 components, 2nd is country
                    location.CountryName = splits[1];

                    break;
                }
                case 3:
                {
                    // 3 components is platform/site/country
                    location.SiteName = splits[1];
                    location.CountryName = splits[2];

                    break;
                }
                default:
                {
                    // 4+ is platform/site/region/country ...
                    location.SiteName = splits[1];
                    location.RegionName = splits[2];
                    location.CountryName = splits[3];

                    break;
                }
            }

            return location;
        }
    }
}
