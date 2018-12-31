using System;

namespace Framework.MessageBroker.RabbitMQ
{
    /// <summary>
    /// Interface de subscriber para o RabbitMQ
    /// </summary>
    public interface IRabbitMQSubscriber : ISubscriber, IDisposable
    {
        
    }
}