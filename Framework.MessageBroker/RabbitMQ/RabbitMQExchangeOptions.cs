using System;
using System.Linq;

namespace Framework.MessageBroker.RabbitMQ
{
    public class RabbitMQExchangeOptions
    {
        public string ExchangeName { get; set; }

        public string QueueName { get; set; }

        public string RoutingKey { get; set; }

        public string ExchangeType { get; set; }

        public bool Durable { get; set; }

        public static RabbitMQExchangeOptions Build<T>() where T : BaseMessage
        {
            var type = typeof(T);
            var defaultQueueName = type.FullName;
            var defaultExchangeName = $"{type.FullName}.Exchange";

            //Validar se foi informado o atributo com configurações cutomizadas
            var attr = type.GetCustomAttributes(typeof(RabbitMQPropertiesAttribute), false);

            if (!attr.Any())
            {
                return new RabbitMQExchangeOptions
                {
                    ExchangeName = string.Empty,
                    QueueName = defaultQueueName,
                    ExchangeType = "default",
                    RoutingKey = string.Empty
                };
            }

            var info = attr[0] as RabbitMQPropertiesAttribute;

            //Se tipo da exchange for default, criaremos somente a fila, pois a exchange default
            //já é pré-declarada pelo Rabbit
            var exchangeName = string.Empty;

            //Caso seja diferente de default e tenha sido informado um nome
            if (string.IsNullOrWhiteSpace(info.ExchangeName) && info.ExchangeType != EExchangeType.Default)
                exchangeName = defaultExchangeName;
            else if (!string.IsNullOrWhiteSpace(info.ExchangeName))
                exchangeName = info.ExchangeName;

            //Validar se devemos criar automáticamente o nome da fila
            var queueName = defaultQueueName;

            if (info.GenerateQueueName)
                queueName = string.Empty;
            else
                queueName = string.IsNullOrWhiteSpace(info.QueueName) ? defaultQueueName : info.QueueName; //Se foi informado um nome para a fila, usar.


            if (info.ExchangeType != EExchangeType.Default && string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentNullException("ExchangeName", "O nome da Exchange deve ser informado.");

            return new RabbitMQExchangeOptions
            {
                ExchangeName = exchangeName,
                QueueName = queueName,
                ExchangeType = info.ExchangeType.GetName(),
                RoutingKey = info.RoutingKey,
                Durable = info.Durable
            };
        }
    }
}