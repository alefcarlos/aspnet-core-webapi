using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;


namespace Demo.Worker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public ServiceProvider ServiceProvier { get; }

        public Startup()
        {
            //Obter a env
            var envName = Environment.GetEnvironmentVariable("ENVIRONMENT_NAME");

            //setup our configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            if (!string.IsNullOrWhiteSpace(envName))
                builder.AddJsonFile($"appsettings.{envName}.json", optional: true);

            Configuration = builder.Build();

            //setup our DI
            var servicesBuilder = new ServiceCollection()
                .AddLogging(config =>
                {
                    config.AddConfiguration(Configuration.GetSection("Logging"));
                    config.AddConsole();
                    //Caso seja necessário logar no EventViewer
                    //Microsoft.Extensions.Logging.EventLog;
                    // config.AddEventLog(new EventLogSettings()
                    // {
                    //     SourceName = "ServiceDiscoveryCache",
                    //     LogName = "ServiceDiscovery",
                    //     Filter = (x, y) => y >= LogLevel.Information
                    // });
                    config.AddDebug();
                });

            ConfigureServices(servicesBuilder);
            ServiceProvier = servicesBuilder.BuildServiceProvider();
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// Obtém um serviço no container de dependências.
        /// </summary>
        /// <typeparam name="T">Tipo do serviço.</typeparam>
        public T GetService<T>() => (T)ServiceProvier.GetService(typeof(T));
    }
}