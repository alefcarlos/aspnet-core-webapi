using Framework.MessageBroker;
using Framework.MessageBroker.RabbitMQ;

namespace Framework.MessageBroker.Tests.RabbitMQ.Messages
{
    [RabbitMQProperties(Durable = true, ExchangeType = EExchangeType.Direct)]
    public class TestDirectMessage : BaseMessage
    {
        public int Idade { get; set; }
    }
}