using Framework.MessageBroker.RabbitMQ.Explorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.MessageBroker.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitBroker(this IServiceCollection services, string appName, IConfiguration configuration, bool addHealthCheck = true)
        {
            var uri = configuration.GetConnectionString("RabbitMQ");

            //Adicionar publisher como singleton, pois devemos sempre compartilhar a conexão TCP
            services.AddSingleton<RabbitMQConnectionWrapper>((provider) => new RabbitMQConnectionWrapper(appName, uri));

            services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();

            //Adicionar como transiente para garantir que NUNCA compartilharemos as intâncias dos channels por thread
            services.AddTransient<IRabbitMQSubscriber, RabbitMQSubscriber>();

            services.AddHttpClient<IRabbitMQExplorer, RabbitMQExplorer>();

            if (addHealthCheck)
            {
                services.AddHealthChecks()
                    .AddRabbitMQ(uri, name: "rabbitmq", tags: new string[] { "messagebroker", "rabbitmq" });
            }

            return services;
        }
    }
}