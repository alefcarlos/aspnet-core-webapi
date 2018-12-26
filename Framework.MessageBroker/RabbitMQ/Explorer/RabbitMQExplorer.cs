using Framework.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MessageBroker.RabbitMQ.Explorer
{
    public class RabbitMQExplorer : IRabbitMQExplorer
    {
        private readonly HttpClient _client;
        private readonly Uri _rabbitMQURI;

        public RabbitMQExplorer(HttpClient client)
        {
            var uri = CommonHelpers.GetValueFromEnv<string>("RABBITMQ_URI");
            _rabbitMQURI = new Uri(uri);

            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var scheme = _rabbitMQURI.Scheme == "amqps" ? "https://" : "http://";
            var port = _rabbitMQURI.IsDefaultPort ? 15672 : _rabbitMQURI.Port;

            _client.BaseAddress = new Uri($"{scheme}{_rabbitMQURI.Host}:{port}");

            var byteArray = Encoding.ASCII.GetBytes(_rabbitMQURI.UserInfo);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<RabbitMQQueueDetail> GetQueue(string name, string vhost = "%f2")
        {
            var response = await _client.GetAsync($"/api/queues/{vhost}/{name}");

            if (!response.IsSuccessStatusCode)
                return null;

            var obj = await response.Content.ReadAsAsync<RabbitMQQueueDetail>();

            return obj;
        }

        public async Task<List<RabbitMQQueueDetail>> GetQueues(string vhost = "%f2")
        {
            var response = await _client.GetAsync("/api/queues/");

            if (!response.IsSuccessStatusCode)
                return null;

            var obj = await response.Content.ReadAsAsync<List<RabbitMQQueueDetail>>();

            return obj;
        }

        public async Task<RabbitMQExchangeDetail> GetExchange(string name, string vhost = "%f2")
        {
            var response = await _client.GetAsync($"/api/exchanges/{vhost}/{name}");

            if (!response.IsSuccessStatusCode)
                return null;

            var obj = await response.Content.ReadAsAsync<RabbitMQExchangeDetail>();

            return obj;
        }

        public async Task<List<RabbitMQExchangeDetail>> GetExchanges(string vhost = "%f2")
        {
            var response = await _client.GetAsync("/api/exchanges/");

            if (!response.IsSuccessStatusCode)
                return null;

            var obj = await response.Content.ReadAsAsync<List<RabbitMQExchangeDetail>>();

            return obj;
        }
    }
}
