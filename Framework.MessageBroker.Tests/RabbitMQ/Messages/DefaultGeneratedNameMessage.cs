using Framework.MessageBroker.RabbitMQ;

namespace Framework.MessageBroker.Tests.RabbitMQ.Messages
{
    [RabbitMQProperties(GenerateQueueName = true)]
    public class DefaultGeneratedNameMessage : BaseMessage
    {
        public string Campo { get; set; }
    }
}
