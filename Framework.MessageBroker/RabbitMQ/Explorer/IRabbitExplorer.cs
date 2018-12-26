using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.MessageBroker.RabbitMQ.Explorer
{
    public interface IRabbitMQExplorer
    {
        Task<List<RabbitMQQueueDetail>> GetQueues(string vhost = "%2f");
        Task<RabbitMQQueueDetail> GetQueue(string name, string vhost = "%2f");

        Task<List<RabbitMQExchangeDetail>> GetExchanges(string vhost = "%2f");
        Task<RabbitMQExchangeDetail> GetExchange(string exchangeName, string vhost = "%2f");
    }
}
