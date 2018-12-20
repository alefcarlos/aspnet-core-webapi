using Microsoft.Extensions.DependencyInjection;

namespace Framework.MessageBroker.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, string appName)
        {
            //Cria conexao singleton do RabbitMQ
            services.AddSingleton<RabbitMQConnectionWrapper>((provider) => new RabbitMQConnectionWrapper(appName));

            //Adicionar publisher
            services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();

            return services;
        }
    }
}