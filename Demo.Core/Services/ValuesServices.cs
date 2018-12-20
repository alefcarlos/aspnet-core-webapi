using System.Threading.Tasks;
using Demo.Core.Contracts.Values;
using Demo.Core.Messages.RabbitMQ;
using Framework.MessageBroker.RabbitMQ;
using Framework.Services;

namespace Demo.Core.Services
{
    public class ValuesServices :  BaseServices, IValuesServices
    {
        private readonly IRabbitMQPublisher _publisher;

        public ValuesServices(IRabbitMQPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task<ServicesResult> PostRabbitMessageAsync(PostMessageRequest request)
        {
            var message = new TesteMessage
            {
                Campo = request.Campo
            };

            await _publisher.PublishAsync(message);

            return Created(message.MessageId);
        }
    }
}