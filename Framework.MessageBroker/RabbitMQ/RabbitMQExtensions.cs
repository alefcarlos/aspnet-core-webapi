using Framework.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.MessageBroker.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitBroker(this IServiceCollection services, string appName)
        {
            //Cria conexao singleton do RabbitMQ
            services.AddSingleton<RabbitMQConnectionWrapper>((provider) => new RabbitMQConnectionWrapper(appName));

            //Adicionar publisher
            services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();

            var uri = CommonHelpers.GetValueFromEnv<string>("RABBITMQ_URI");

            services.AddHealthChecks()
                .AddRabbitMQ(uri, "rabbitmq", tags: new string[] { "messagebroker", "rabbitmq" });

            return services;
        }
    }
}