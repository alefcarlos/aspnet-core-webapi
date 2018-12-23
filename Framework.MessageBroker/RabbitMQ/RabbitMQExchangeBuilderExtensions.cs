using RabbitMQ.Client;

namespace Framework.MessageBroker.RabbitMQ
{
    public static class RabbitMQExchangeBuilderExtensions
    {
        public static void CreateModels(this IModel channel, RabbitMQExchangeOptions options, bool bindQueueToExchange = false)
        {
            if (options.ExchangeType == "default")
                options.QueueName = channel.CreateQueue(options); // Garantir que o nome da fila seja propagado
            else
            {
                channel.CreateExchange(options);

                if (bindQueueToExchange)
                {
                    channel.CreateQueue(options);
                    channel.BindQueue(options);
                }
            }
        }

        public static string CreateQueue(this IModel channel, RabbitMQExchangeOptions options)
        {
            var queue = channel.QueueDeclare(queue: options.QueueName,
                           durable: options.Durable,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

            return queue.QueueName;
        }

        public static void CreateExchange(this IModel channel, RabbitMQExchangeOptions options)
        {
            channel.ExchangeDeclare(exchange: options.ExchangeName,
                                    type: options.ExchangeType,
                                    durable: options.Durable,
                                    autoDelete: false,
                                    arguments: null);
        }

        public static void BindQueue(this IModel channel, RabbitMQExchangeOptions options)
        {
            channel.QueueBind(queue: options.QueueName,
                              exchange: options.ExchangeName,
                              routingKey: options.RoutingKey);
        }
    }
}