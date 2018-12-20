using System.Threading.Tasks;

namespace Framework.MessageBroker
{
    public interface IPublisher
    {
        void Publish<T>(T model) where T : BaseMessage;
        Task PublishAsync<T>(T model) where T : BaseMessage;
    }
}