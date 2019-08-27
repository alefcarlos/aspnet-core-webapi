using Framework.MessageBroker.RabbitMQ;
using Framework.MessageBroker.RabbitMQ.Explorer;
using Framework.MessageBroker.Tests.RabbitMQ.Messages;
using Framework.Test;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Framework.MessageBroker.Tests.RabbitMQ
{
    public class Tests : TestHost<Startup>
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
            subscriber.StartConsume<DefaultMessage>((msg) =>
            {
                publishWithSuccess = msg.Campo == message.Campo;
                return true;
            });
            await _publisher.PublishAsync(message);

            Thread.Sleep(2000);
            subscriber.Dispose();

            //Assert
            options.QueueName.ShouldNotBeNullOrWhiteSpace();

            var queueGenerated = await _rabbitExplorer.GetQueue(options.QueueName);

            queueGenerated.ShouldNotBeNull();
            queueGenerated.name.ShouldBe(options.QueueName);
            queueGenerated.durable.ShouldBe(options.Durable);
            publishWithSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task PublisheAndConsumeNamed()
        {
            //Arrange
            var subscriber = GetService<IRabbitMQSubscriber>();
            bool publishWithSuccess = false;
            var message = new NamedMessage
            {
                Campo = "PublisheAndConsumeNamd"
            };

            //Act
            var options = subscriber.StartConsume<NamedMessage>((msg) =>
            {
                publishWithSuccess = msg.Campo == message.Campo;
                return true;
            });
            var rabbitOptions = options as RabbitMQExchangeOptions;

            await _publisher.PublishAsync(message, rabbitOptions.QueueName);

            Thread.Sleep(2000);
            subscriber.Dispose();

            //Assert
            rabbitOptions.QueueName.ShouldNotBeNullOrWhiteSpace();

            var queueGenerated = await _rabbitExplorer.GetQueue(rabbitOptions.QueueName);

            queueGenerated.ShouldNotBeNull();
            queueGenerated.name.ShouldBe(rabbitOptions.QueueName);
            queueGenerated.durable.ShouldBe(rabbitOptions.Durable);
            publishWithSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task PublisheAndConsumeDirectExchange()
        {
            //Arrange
            var subscriber = GetService<IRabbitMQSubscriber>();
            bool publishWithSuccess = false;
            var message = new DirectMessage
            {
                Idade = 42,
                Campo = "PublisheAndConsumeDirectExchange"
            };
            var options = RabbitMQExchangeOptions.Build<DirectMessage>();

            //Act
            subscriber.StartConsume<DirectMessage>((msg) =>
            {
                publishWithSuccess = msg.Campo == message.Campo;
                return true;
            });
            await _publisher.PublishAsync(message);

            Thread.Sleep(2000);
            subscriber.Dispose();

            //Assert
            options.QueueName.ShouldNotBeNullOrWhiteSpace();
            options.ExchangeName.ShouldNotBeNullOrWhiteSpace();

            var queueGenerated = await _rabbitExplorer.GetQueue(options.QueueName);
            var exchangeGenerated = await _rabbitExplorer.GetExchange(options.ExchangeName);

            queueGenerated.ShouldNotBeNull();
            exchangeGenerated.ShouldNotBeNull();

            exchangeGenerated.name.ShouldBe(options.ExchangeName);
            exchangeGenerated.type.ShouldBe(options.ExchangeType);
            exchangeGenerated.durable.ShouldBe(options.Durable);

            queueGenerated.name.ShouldBe(options.QueueName);
            queueGenerated.durable.ShouldBe(options.Durable);
            publishWithSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task PublisheAndConsumeDefaultGeneratedName()
        {
            //Arrange
            var subscriber = GetService<IRabbitMQSubscriber>();
            bool publishWithSuccess = false;
            var message = new DefaultGeneratedNameMessage
            {
                Campo = "PublisheAndConsumeDefaultGeneratedName"
            };

            //Act
            var options = subscriber.StartConsume<DefaultGeneratedNameMessage>((msg) =>
            {
                publishWithSuccess = msg.Campo == message.Campo;
                return true;
            });
            var rabbitOptions = options as RabbitMQExchangeOptions;

            await _publisher.PublishAsync(message, rabbitOptions.QueueName);

            Thread.Sleep(2000);
            subscriber.Dispose();

            //Assert
            rabbitOptions.QueueName.ShouldNotBeNullOrWhiteSpace();

            var queueGenerated = await _rabbitExplorer.GetQueue(rabbitOptions.QueueName);

            queueGenerated.ShouldNotBeNull();
            queueGenerated.name.ShouldBe(rabbitOptions.QueueName);
            queueGenerated.durable.ShouldBe(rabbitOptions.Durable);
            publishWithSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task PublisheAndConsumeDirectGeneratedName()
        {
            //Arrange
            var subscriber = GetService<IRabbitMQSubscriber>();
            bool publishWithSuccess = false;
            var message = new DirectedGeneratedNameMessage
            {
                Campo = "DirectedGeneratedNameMessage"
            };

            //Act
            var _options = subscriber.StartConsume<DirectedGeneratedNameMessage>((msg) =>
            {
                publishWithSuccess = msg.Campo == message.Campo;
                return true;
            });
            await _publisher.PublishAsync(message);

            Thread.Sleep(2000);
            subscriber.Dispose();

            //Assert
            var options = _options as RabbitMQExchangeOptions;

            options.QueueName.ShouldNotBeNullOrWhiteSpace();
            options.ExchangeName.ShouldNotBeNullOrWhiteSpace();

            var queueGenerated = await _rabbitExplorer.GetQueue(options.QueueName);
            var exchangeGenerated = await _rabbitExplorer.GetExchange(options.ExchangeName);

            queueGenerated.ShouldNotBeNull();
            exchangeGenerated.ShouldNotBeNull();

            exchangeGenerated.name.ShouldBe(options.ExchangeName);
            exchangeGenerated.type.ShouldBe(options.ExchangeType);
            exchangeGenerated.durable.ShouldBe(options.Durable);

            queueGenerated.name.ShouldBe(options.QueueName);
            queueGenerated.durable.ShouldBe(options.Durable);
            publishWithSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task PublisheAndConsumeDefaultRejectedMessage()
        {
            //Arrange
            var subscriber = GetService<IRabbitMQSubscriber>();
            bool publishWithSuccess = false;
            int count = 0;

            var message = new DefaultRejectedMessage
            {
                Campo = "PublisheAndConsumeDefaultRejectedMessage"
            };
            var options = RabbitMQExchangeOptions.Build<DefaultRejectedMessage>();

            //Act
            subscriber.StartConsume<DefaultRejectedMessage>((msg) =>
            {
                publishWithSuccess = msg.Campo == message.Campo;

                return ++count == 2;
            });
            await _publisher.PublishAsync(message);

            Thread.Sleep(2000);
            subscriber.Dispose();

            //Assert
            options.QueueName.ShouldNotBeNullOrWhiteSpace();

            var queueGenerated = await _rabbitExplorer.GetQueue(options.QueueName);

            queueGenerated.ShouldNotBeNull();
            queueGenerated.name.ShouldBe(options.QueueName);
            queueGenerated.durable.ShouldBe(options.Durable);
            publishWithSuccess.ShouldBeTrue();
            count.ShouldBe(2);
        }
    }
}