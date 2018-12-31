using System;

namespace Framework.MessageBroker
{
    /// <summary>
    /// Tipo base para todos os tipos de mensagem.
    /// </summary>
    public class BaseMessage
    {
        public BaseMessage()
        {
        }

        /// <summary>
        /// ID da mensagem.
        /// É gerado no momento da chamada do Publish.
        /// </summary>
        public Guid MessageId { get; set; }
    }
}