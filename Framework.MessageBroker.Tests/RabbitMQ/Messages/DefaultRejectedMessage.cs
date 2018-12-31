using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.MessageBroker.Tests.RabbitMQ.Messages
{
    public class DefaultRejectedMessage : BaseMessage
    {
        public string Campo { get; set; }
    }
}
