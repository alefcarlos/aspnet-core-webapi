using System;

namespace Framework.MessageBroker
{
    public class BaseMessage
    {
        public BaseMessage()
        {
            MessageId = Guid.NewGuid();
        }

        public Guid MessageId { get; set; }
    }
}