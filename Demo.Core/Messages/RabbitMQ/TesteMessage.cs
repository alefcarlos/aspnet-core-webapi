using Framework.MessageBroker;

namespace Demo.Core.Messages.RabbitMQ
{
    public class TesteMessage : BaseMessage
    {
        public string Campo { get; set; }
    }
}