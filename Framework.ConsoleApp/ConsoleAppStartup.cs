using dotenv.net;
using Framework.Core.Serializer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Framework.ConsoleApp
{
    public abstract class ConsoleAppStartup
    {
        public void Run()
        {
            if (File.Exists(".env"))
                DotEnv.Config();

            BuildHost()
                .Run();
        }
        private IHost BuildHost()
        {
            var host = new HostBuilder()
               .ConfigureLogging((hostContext, configLogging) =>
               {
                   configLogging.AddConsole();
                   configLogging.AddDebug();
               })
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddSingleton<JsonSerializerCommon>();
                   ConfigureServices(services);
               })
               .UseConsoleLifetime()
               .Build();

            return host;
        }

        public abstract void ConfigureServices(IServiceCollection services);
    }
}