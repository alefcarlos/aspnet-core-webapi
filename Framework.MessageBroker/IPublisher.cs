using System.Threading.Tasks;

namespace Framework.MessageBroker
{
    public interface IPublisher
    {
        void Publish<T>(T model) where T : BaseMessage;
        Task PublishAsync<T>(T model) where T : BaseMessage;

        void Publish<T>(T model, string queueName) where T : BaseMessage;
        Task PublishAsync<T>(T model, string queueName) where T : BaseMessage;
    }
}