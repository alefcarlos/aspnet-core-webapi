using System;

namespace Framework.MessageBroker.RabbitMQ
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RabbitMQPropertiesAttribute : System.Attribute
    {
        public RabbitMQPropertiesAttribute()
        {
            ExchangeName = string.Empty;
            QueueName = string.Empty;
            RoutingKey = string.Empty;
        }

        public string ExchangeName { get; set; }

        public bool GenerateQueueName { get; set; }

        public string QueueName { get; set; }

        public string RoutingKey { get; set; }

        public EExchangeType ExchangeType { get; set; }

        public bool Durable { get; set; }
    }

    public enum EExchangeType
    {
        Default,
        Direct,
        Fanout
    }

    public static class ExchangeTypeExtensions
    {
        public static string GetName(this EExchangeType enm)
        {

            switch (enm)
            {
                case EExchangeType.Default:
                    return "default";

                case EExchangeType.Direct:
                    return "direct";

                case EExchangeType.Fanout:
                    return "fanout";

                default:
                    throw new Exception("O tipo de Exchange não é suportado.");
            }
        }
    }
}