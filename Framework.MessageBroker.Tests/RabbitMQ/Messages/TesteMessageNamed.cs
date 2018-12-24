using Framework.MessageBroker;
using Framework.MessageBroker.RabbitMQ;

namespace Framework.MessageBroker.Tests.RabbitMQ.Messages
{
    [RabbitMQProperties(Durable = true, QueueName = "test_named", ExchangeType = EExchangeType.Default)]
    public class TesteMessageNamed : BaseMessage
    {
        public string Campo { get; set; }
    }
}