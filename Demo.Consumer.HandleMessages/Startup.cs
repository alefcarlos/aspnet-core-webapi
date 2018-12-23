using Framework.ConsoleApp;
using Framework.MessageBroker.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Consumer.HandleMessages
{
    public class Startup : ConsoleAppStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitBroker("demo.consumer", false);
            services.AddHostedService<TesteMessageNamedHandler>();
        }
    }
}