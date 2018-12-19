using System.Net.Http;

namespace Framework.Data.ApiClient
{
    /// <summary>
    /// Classe base para serviços que utilizam requisições HTTP
    /// </summary>
    public class ApiClientBase
    {
        protected readonly HttpClient _client;

        public ApiClientBase(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
        }
    }
}