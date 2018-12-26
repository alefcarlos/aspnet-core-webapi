using Framework.MessageBroker;
using Framework.MessageBroker.RabbitMQ;

namespace Framework.MessageBroker.Tests.RabbitMQ.Messages
{
    [RabbitMQProperties(Durable = true, QueueName = "test_named")]
    public class NamedMessage : BaseMessage
    {
        public string Campo { get; set; }
    }
}