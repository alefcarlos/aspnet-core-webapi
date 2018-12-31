using Framework.Core.Helpers;
using Framework.Core.Serializer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Framework.MessageBroker.RabbitMQ
{
    public class RabbitMQSubscriber : IRabbitMQSubscriber
    {
        private readonly IConnection _connection;
        private readonly JsonSerializerCommon _serializer;
        private readonly ILogger _logger;

        private IModel _channel;

        public RabbitMQSubscriber(RabbitMQConnectionWrapper connection, JsonSerializerCommon serializer, ILogger<RabbitMQSubscriber> logger)
        {
            _connection = connection.Connection;
            _serializer = serializer;
            _logger = logger;
        }

        private T DefaultMsgBinder<T>(byte[] data)
        {
            var message = default(T);
            var json = "";

            if (data != null)
            {
                json = Encoding.UTF8.GetString(data);
                message = _serializer.Deserialize<T>(json);
            }

            return message;
        }

        public IExchangeOptions StartConsume<T>(Func<T, bool> factory, Func<byte[], T> msgBinder = null) where T : BaseMessage
        {
            _channel = _connection.CreateModel();

            var options = RabbitMQExchangeOptions.Build<T>();

            //Devemos realizar as associações da queue/exchange
            _channel.CreateModels(options, true);

            //Verificar se existe env configurando o limite de mensagens
            var limit = CommonHelpers.GetValueFromEnv<ushort>("RABBIT_QOS", false);

            if (limit > 0)
                _channel.BasicQos(0, limit, false); //Limtiar por consumer

            _logger.LogInformation("Waiting for messages.");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (m, ea) =>
            {
                T message;

                if (msgBinder != null)
                    message = msgBinder(ea.Body);
                else
                    message = DefaultMsgBinder<T>(ea.Body);

                _logger.LogInformation($"New message, id {message.MessageId}");

                var result = factory(message);


                if (result)
                    _channel.BasicAck(ea.DeliveryTag, false); //Devemos indicar que a mensagem foi processado com sucesso.
                else
                    //Devemos enviar a mensagem para a fila novamente, assim pode ser processado por outra instância desse consumer
                    _channel.BasicReject(ea.DeliveryTag, true);
            };

            _channel.BasicConsume(queue: options.QueueName,
                autoAck: false,
                consumer: consumer);

            return options;
        }

        public void Dispose()
        {
            if (_channel != null && _channel.IsOpen)
            {
                _channel.Close();
                _channel.Dispose();
            }
        }
    }
}