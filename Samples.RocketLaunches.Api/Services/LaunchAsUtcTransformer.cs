using System;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class LaunchAsUtcTransformer : ITransformer<CsvLaunch, Launch>
    {
        public Launch To(CsvLaunch source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // Basically going to take the launch date info from the csv file (which does NOT contain any identifying
            // TZ information) and assume that the time from the CSV is represented in UTC time...
            var convertedDateTime = source.LaunchDateTime.ToDateTime() ?? throw new ApplicationException("CsvLaunch source missing LaunchDateTime valud");

            return new Launch
                   {
                       CompanyId = source.CompanyId ?? throw new ApplicationException("CsvLaunch source missing CompanyId valud"),
                       StatusId = source.StatusId ?? throw new ApplicationException("CsvLaunch source missing StatusId valud"),
                       LocationId = source.LocationId ?? throw new ApplicationException("CsvLaunch source missing LocationId valud"),
                       Name = source.Name ?? throw new ApplicationException("CsvLaunch source missing Name valud"),
                       Cost = source.Cost,
                       LaunchedOnUtc = convertedDateTime.AsUtc().ToUnixTimestamp()
                   };
        }
    }
}
