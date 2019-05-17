using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Messages.RabbitMQ;
using Framework.MessageBroker.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.Consumer.HandleMessages.Tasks
{
    public class TesteMessageHandlerTask : IHostedService
    {
        private readonly IRabbitMQSubscriber _subscriber;
        private readonly ILogger _logger;

        public TesteMessageHandlerTask(IRabbitMQSubscriber subscriber, ILogger<TesteMessageHandlerTask> logger)
        {
            _subscriber = subscriber;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Realizar bind do consummo da fila
            _subscriber.StartConsume<TesteMessage>(ConsumeTesteMessage);
            return Task.CompletedTask;
        }

        private bool ConsumeTesteMessage(TesteMessage message)
        {
            _logger.LogInformation($"ConsumeTesteMessage, Campo = {message.Campo}");
            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.Dispose();
            return Task.CompletedTask;
        }
    }
}
