using System.IO;
using dotenv.net;
using Framework.Core.Serializer;
using Framework.MessageBroker.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.Consumer.HandleMessages
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(".env"))
                DotEnv.Config();

            var host = new HostBuilder()
               .ConfigureLogging((hostContext, configLogging) =>
               {
                   configLogging.AddConsole();
                   configLogging.AddDebug();
               })
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddSingleton<JsonSerializerCommon>();
                   services.AddRabbitBroker("demo.consumer", false);
                   services.AddHostedService<TesteMessageHandler>();
                   services.AddHostedService<TesteMessageNamedHandler>();
                   services.AddHostedService<TestDirectMessageHandler>();
               })
               .UseConsoleLifetime()
               .Build();

            host.Run();
        }
    }
}
