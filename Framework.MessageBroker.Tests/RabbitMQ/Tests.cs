using System.Threading;
using Framework.MessageBroker.RabbitMQ;
using Framework.MessageBroker.Tests.RabbitMQ.Messages;
using Framework.Test;
using Xunit;
using Shouldly;

namespace Framework.MessageBroker.Tests.RabbitMQ
{
    public class Tests : BaseTest<Startup>
    {
        private readonly IRabbitMQPublisher _publisher;
        public Tests()
        {
            _publisher = GetService<IRabbitMQPublisher>();
        }

        [Fact]
        public void PublisheAndConsumeDefault()
        {
            //Arrange
            var subscriber = GetService<IRabbitMQSubscriber>();
            bool publishWithSuccess = false;
            var message = new DefaultMessage
            {
                Campo = "PublisheAndConsumeDefault"
            };

            //Act
            _publisher.Publish(message);
            subscriber.StartConsume<DefaultMessage>((msg) =>
            {
                publishWithSuccess = msg.MessageId == message.MessageId && msg.Campo == message.Campo;
                return true;
            });

            Thread.Sleep(1000);
            subscriber.Dispose();

            //Assert
            publishWithSuccess.ShouldBeTrue();
        }
    }
}