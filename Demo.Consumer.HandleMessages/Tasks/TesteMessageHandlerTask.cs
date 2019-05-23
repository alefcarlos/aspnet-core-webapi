using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Messages.RabbitMQ;
using Framework.MessageBroker.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.Consumer.HandleMessages.Tasks
{
    public class TesteMessageHandlerTask : BackgroundService
    {
        private readonly IRabbitMQSubscriber _subscriber;
        private readonly ILogger _logger;

        public TesteMessageHandlerTask(IRabbitMQSubscriber subscriber, ILogger<TesteMessageHandlerTask> logger)
        {
            _subscriber = subscriber;
            _logger = logger;
        }

        public override void Dispose(){
            _subscriber.Dispose();
            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.StartConsume<TesteMessage>(ConsumeTesteMessage);
            return Task.CompletedTask;
        }

        private bool ConsumeTesteMessage(TesteMessage message)
        {
            _logger.LogInformation($"ConsumeTesteMessage, Campo = {message.Campo}");
            return true;
        }
    }
}
