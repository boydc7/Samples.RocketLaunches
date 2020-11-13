using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Samples.RocketLaunches.Api.DataAccess;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public class DemoDataService : IDemoDataService
    {
        private readonly ILaunchRepository _launchRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IStatusRepository _statusRepository;

        public DemoDataService(ILaunchRepository launchRepository,
                               ILocationRepository locationRepository,
                               ICompanyRepository companyRepository,
                               IStatusRepository statusRepository)
        {
            _launchRepository = launchRepository;
            _locationRepository = locationRepository;
            _companyRepository = companyRepository;
            _statusRepository = statusRepository;
        }

        public async Task CreateDemoDataAsync()
        {
            // Companies
            using(var reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "rocket_companies.csv")))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await foreach (var company in csv.GetRecordsAsync<Company>())
                {
                    _companyRepository.Add(company);
                }
            }

            // Launches
            using(var reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "rocket_launches.csv")))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await foreach (var launch in csv.GetRecordsAsync<Launch>())
                {
                    _launchRepository.Add(launch);
                }
            }

            // Locations
            using(var reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "launch_location.csv")))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await foreach (var location in csv.GetRecordsAsync<Location>())
                {
                    _locationRepository.Add(location);
                }
            }

            // Status
            using(var reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "mission_status.csv")))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await foreach (var status in csv.GetRecordsAsync<Status>())
                {
                    _statusRepository.Add(status);
                }
            }
        }
    }
}
