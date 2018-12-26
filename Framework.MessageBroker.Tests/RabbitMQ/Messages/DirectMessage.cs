using Framework.MessageBroker;
using Framework.MessageBroker.RabbitMQ;

namespace Framework.MessageBroker.Tests.RabbitMQ.Messages
{
    [RabbitMQProperties(Durable = true, ExchangeType = EExchangeType.Direct)]
    public class DirectMessage : BaseMessage
    {
        public int Idade { get; set; }

        public string Campo { get; set; }
    }
}