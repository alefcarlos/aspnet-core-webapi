using Framework.MessageBroker.RabbitMQ;

namespace Framework.MessageBroker.Tests.RabbitMQ.Messages
{
    [RabbitMQProperties(GenerateQueueName = true, ExchangeType = EExchangeType.Direct)]
    public class DirectedGeneratedNameMessage : BaseMessage
    {
        public string Campo { get; set; }
    }
}
