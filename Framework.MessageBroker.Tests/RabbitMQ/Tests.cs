using Framework.MessageBroker.RabbitMQ;
using Framework.MessageBroker.RabbitMQ.Explorer;
using Framework.MessageBroker.Tests.RabbitMQ.Messages;
using Framework.Test;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Framework.MessageBroker.Tests.RabbitMQ
{
    public class Tests : BaseTest<Startup>
    {
        private readonly IRabbitMQPublisher _publisher;
        private readonly IRabbitMQExplorer _rabbitExplorer;

        public Tests()
        {
            _publisher = GetService<IRabbitMQPublisher>();
            _rabbitExplorer = GetService<IRabbitMQExplorer>();
        }

        [Fact]
        public async Task PublisheAndConsumeDefault()
        {
            //Arrange
            var subscriber = GetService<IRabbitMQSubscriber>();
            bool publishWithSuccess = false;
            var message = new DefaultMessage
            {
                Campo = "PublisheAndConsumeDefault"
            };
            var options = RabbitMQExchangeOptions.Build<DefaultMessage>();

            //Act
            var queueGenerated = await _rabbitExplorer.GetQueue(options.QueueName);

            await _publisher.PublishAsync(message);
            subscriber.StartConsume<DefaultMessage>((msg) =>
            {
                publishWithSuccess = msg.MessageId == message.MessageId && msg.Campo == message.Campo;
                return true;
            });

            Thread.Sleep(1000);
            subscriber.Dispose();

            //Assert
            queueGenerated.ShouldNotBeNull();
            queueGenerated.name.ShouldBe(options.QueueName);
            queueGenerated.durable.ShouldBe(options.Durable);
            publishWithSuccess.ShouldBeTrue();
        }
    }
}