using System;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.Serializer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

        public void StartConsume<T>(Func<T, bool> factory, Func<BaseMessage, T> msgBinder = null, TaskCreationOptions runningOpt = TaskCreationOptions.None) where T : BaseMessage
        {
            _channel = _connection.CreateModel();

            var options = RabbitMQExchangeOptions.Build<T>();

            //Devemos realizar as associações da queue/exchange
            _channel.CreateModels(options, true);

            _logger.LogInformation("Waiting for messages.");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (m, ea) =>
            {
                var message = default(T);
                var json = "";

                if (ea.Body != null)
                {
                    json = Encoding.UTF8.GetString(ea.Body);
                    message = _serializer.Deserialize<T>(json);
                }

                _logger.LogInformation($"New message, id {message.MessageId}");


                var result = factory(message);

                if (result)
                {
                    _logger.LogInformation($"Message id {message.MessageId} is processed, acking....");
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(queue: options.QueueName,
                autoAck: false,
                consumer: consumer);
        }

        public void Dispose()
        {
            if (_channel != null && _channel.IsOpen)
                _channel.Dispose();
        }
    }
}