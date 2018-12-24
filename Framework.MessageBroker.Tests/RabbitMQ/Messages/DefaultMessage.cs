using Framework.MessageBroker;
using Framework.MessageBroker.RabbitMQ;

namespace Framework.MessageBroker.Tests.RabbitMQ.Messages
{
    public class DefaultMessage : BaseMessage
    {
        public string Campo { get; set; }
    }
}