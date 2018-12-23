using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Messages.RabbitMQ;
using Framework.MessageBroker.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.Consumer.HandleMessages
{
    public class TesteMessageNamedHandler : IHostedService
    {
        private readonly IRabbitMQSubscriber _subscriber;
        private readonly ILogger _logger;

        public TesteMessageNamedHandler(IRabbitMQSubscriber subscriber, ILogger<TesteMessageHandler> logger)
        {
            _subscriber = subscriber;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Realizar bind do consummo da fila
            _subscriber.StartConsume<TesteMessageNamed>(ConsumeTesteMessageNamed);
            return Task.CompletedTask;
        }

        private bool ConsumeTesteMessageNamed(TesteMessageNamed message)
        {
            _logger.LogInformation($"ConsumeTesteMessageNamed, Campo = {message.Campo}");
            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.Dispose();
            return Task.CompletedTask;
        }
    }
}