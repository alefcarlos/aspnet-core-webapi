using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Messages.RabbitMQ;
using Framework.MessageBroker.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.Consumer.HandleMessages
{
    public class TestDirectMessageHandler : IHostedService
    {
        private readonly IRabbitMQSubscriber _subscriber;
        private readonly ILogger _logger;

        public TestDirectMessageHandler(IRabbitMQSubscriber subscriber, ILogger<TesteMessageHandler> logger)
        {
            _subscriber = subscriber;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Realizar bind do consummo da fila
            _subscriber.StartConsume<TestDirectMessage>(ConsumeTestDirectMessage);
            return Task.CompletedTask;
        }

        private bool ConsumeTestDirectMessage(TestDirectMessage message)
        {
            _logger.LogInformation($"ConsumeTestDirectMessage, Idade = {message.Idade}");
            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.Dispose();
            return Task.CompletedTask;
        }
    }
}