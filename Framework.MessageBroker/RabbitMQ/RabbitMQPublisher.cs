using System.Text;
using System.Threading.Tasks;
using Framework.Core.Serializer;
using RabbitMQ.Client;

namespace Framework.MessageBroker.RabbitMQ
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IConnection _connection;
        private readonly JsonSerializerCommon _serializer;

        public RabbitMQPublisher(RabbitMQConnectionWrapper connection, JsonSerializerCommon serializer)
        {
            _connection = connection.Connection;
            _serializer = serializer;
        }

        public void Publish<T>(T model) where T : BaseMessage
        {
            var queueName = typeof(T).FullName;
            var json = _serializer.Serialize(model);
            var encoded = Encoding.UTF8.GetBytes(json);

            using (var channel = _connection.CreateModel())
            {
                QueueDeclare(channel, queueName);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(
                                    exchange: string.Empty,
                                    routingKey: queueName,
                                    basicProperties: properties,
                                    body: encoded);
            }

        }

        public async Task PublishAsync<T>(T model) where T : BaseMessage
        {
            await Task.Run(() => { Publish(model); });
        }

        private void QueueDeclare(IModel channel, string name)
        {
            channel.QueueDeclare(queue: name,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
        }
    }
}