using Framework.MessageBroker;
using Framework.MessageBroker.RabbitMQ;

namespace Demo.Core.Messages.RabbitMQ
{
    [RabbitMQProperties(Durable = true, QueueName = "test_named", ExchangeType = EExchangeType.Default)]
    public class TesteMessageNamed : BaseMessage
    {
        public string Campo { get; set; }
    }
}