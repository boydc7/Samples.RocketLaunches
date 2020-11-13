using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Samples.RocketLaunches.Api.DataAccess;
using Samples.RocketLaunches.Api.Filters;
using Samples.RocketLaunches.Api.Services;

namespace Samples.RocketLaunches.Api
{
    internal class ApiStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o => { o.Filters.Add(new ModelAttributeValidationFilter()); })
                    .AddJsonOptions(x =>
                                    {
                                        x.JsonSerializerOptions.IgnoreNullValues = true;
                                        x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                                        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                                        x.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                                        x.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                                        x.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                                        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSwaggerGen(c =>
                                   {
                                       c.SwaggerDoc("rocketlaunch", new OpenApiInfo
                                                                    {
                                                                        Version = "v1",
                                                                        Title = "RocketLaunch Reporting API",
                                                                        Description = "RocketLaunch service related APIs"
                                                                    });

                                       foreach (var xmlFile in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly))
                                       {
                                           c.IncludeXmlComments(xmlFile);
                                       }
                                   });

            // Services
            services.AddSingleton<IDemoDataService, DemoDataService>();

            // Repos
            services.AddSingleton<ILaunchRepository, InMemoryLaunchRepository>()
                    .AddSingleton<ILocationRepository, InMemoryLocationRepository>()
                    .AddSingleton<ICompanyRepository, InMemoryCompanyRepository>()
                    .AddSingleton<IStatusRepository, InMemoryStatusRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); })
               .UseSwagger()
               .UseSwaggerUI(o =>
                             {
                                 o.SwaggerEndpoint("/swagger/rocketlaunch/swagger.json", "RocketLaunch Reporting  API");
                                 o.RoutePrefix = string.Empty;
                             });
        }
    }
}
