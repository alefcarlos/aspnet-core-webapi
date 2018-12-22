using Framework.MessageBroker;
using Framework.MessageBroker.RabbitMQ;

namespace Demo.Core.Messages.RabbitMQ
{
    [RabbitMQProperties(Durable = true, ExchangeType = EExchangeType.Direct)]
    public class TestDirectMessage : BaseMessage
    {
        public int Idade { get; set; }
    }
}