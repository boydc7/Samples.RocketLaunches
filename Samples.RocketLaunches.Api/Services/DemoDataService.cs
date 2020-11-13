using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Logging;
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
        private readonly ITransformer<CsvLocation, Location> _locationTransformer;
        private readonly ITransformer<CsvLaunch, Launch> _launchTransformer;
        private readonly ILogger<DemoDataService> _log;

        public DemoDataService(ILaunchRepository launchRepository,
                               ILocationRepository locationRepository,
                               ICompanyRepository companyRepository,
                               IStatusRepository statusRepository,
                               ITransformer<CsvLocation, Location> locationTransformer,
                               ITransformer<CsvLaunch, Launch> launchTransformer,
                               ILogger<DemoDataService> log)
        {
            _launchRepository = launchRepository;
            _locationRepository = locationRepository;
            _companyRepository = companyRepository;
            _statusRepository = statusRepository;
            _locationTransformer = locationTransformer;
            _launchTransformer = launchTransformer;
            _log = log;
        }

        public async Task CreateDemoDataAsync()
        {
            try
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
            }
            catch(Exception x)
            {
                _log.LogError(x, "Failed trying to load Companies from CSV");
            }

            try
            {
                // Launches
                using(var reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "rocket_launches.csv")))
                using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var id = 1;

                    await foreach (var csvLaunch in csv.GetRecordsAsync<CsvLaunch>())
                    {
                        var launch = _launchTransformer.To(csvLaunch);

                        launch.Id = id;

                        _launchRepository.Add(launch);

                        id++;
                    }
                }
            }
            catch(Exception x)
            {
                _log.LogError(x, "Failed trying to load Launches from CSV");
            }

            try
            {
                // Locations
                using(var reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "launch_location.csv")))
                using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    await foreach (var csvLocation in csv.GetRecordsAsync<CsvLocation>())
                    {
                        var location = _locationTransformer.To(csvLocation);

                        _locationRepository.Add(location);
                    }
                }
            }
            catch(Exception x)
            {
                _log.LogError(x, "Failed trying to load Locations from CSV");
            }

            try
            {
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
            catch(Exception x)
            {
                _log.LogError(x, "Failed trying to load Status from CSV");
            }
        }
    }
}
