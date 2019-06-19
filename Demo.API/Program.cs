using System;
using System.Linq;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Demo.Core.Data.MySql;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.API
{
    public class Program
    {
        public static IMetricsRoot Metrics { get; set; }

        public static void Main(string[] args)
        {
            Metrics = AppMetrics.CreateDefaultBuilder()
                .OutputMetrics.AsPrometheusPlainText()
                // .OutputMetrics.AsPrometheusProtobuf()
                .Build();


            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DbzMySqlContext>();
                    DbzInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    Console.WriteLine("An error occurred while seeding the database.");
                    Console.WriteLine(ex.Message);
                }
            }


            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {

            return WebHost.CreateDefaultBuilder(args)
                        .ConfigureMetrics(Metrics)
                        .UseMetrics(
                            options =>
                            {
                                options.EndpointOptions = endpointsOptions =>
                                {
                                    // endpointsOptions.MetricsTextEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First();
                                    endpointsOptions.MetricsEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First();
                                };
                            })
                .UseStartup<Startup>();
        }
    }
}
