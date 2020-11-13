using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Samples.RocketLaunches.Api.Services;

namespace Samples.RocketLaunches.Api
{
    internal class Program
    {
        private static async Task Main()
        {
            var hostBuilder = new HostBuilder().UseContentRoot(Directory.GetCurrentDirectory())
                                               .ConfigureHostConfiguration(b => b.AddConfiguration(BuildConfiguration))
                                               .ConfigureAppConfiguration((wc, conf) => conf.AddConfiguration(BuildConfiguration))
                                               .ConfigureLogging((x, b) => b.AddConfiguration(x.Configuration.GetSection("Logging"))
                                                                            .AddConsole()
                                                                            .AddDebug()
                                                                            .SetMinimumLevel(LogLevel.Debug))
                                               .ConfigureWebHost(whb => whb.UseShutdownTimeout(TimeSpan.FromSeconds(15))
                                                                           .UseUrls("http://*:8084")
                                                                           .UseKestrel()
                                                                           .UseStartup<ApiStartup>())
                                               .UseConsoleLifetime();

            var host = hostBuilder.Build();

            // Background some seed data
            var demoDataService = host.Services.GetRequiredService<IDemoDataService>();
#pragma warning disable 4014
            demoDataService.CreateDemoDataAsync();
#pragma warning restore 4014

            await host.RunAsync();
        }

        private static IConfiguration BuildConfiguration { get; } = new ConfigurationBuilder().AddJsonFile("appsettings.json", true)
                                                                                              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local"}.json", true, true)
                                                                                              .AddEnvironmentVariables("RLA_")
                                                                                              .Build();
    }
}
