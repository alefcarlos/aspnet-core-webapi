using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Demo.Core.Data.ApiClient.Google.Views;
using System.Web;
using System.Net;
using System.Web.Http;
using Framework.Data.ApiClient;

namespace Demo.Core.Data.ApiClient.Google
{
    /// <summary>
    /// Repositório de acesso as apis do google
    /// </summary>
    public class GoogleMapsRepository : ApiClientBase
    {
        private readonly GoogleApiConfiguration _configuration;


        public GoogleMapsRepository(HttpClient client, GoogleApiConfiguration confis) : base(client)
        {
            _configuration = confis;
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Obtém os dados de localização a partir de um cep
        /// </summary>
        /// <param name="cep">CEP a ser pesquisado</param>
        public async Task<GoogleGeoCodeView> GetGeoCodeByCEPAsync(string cep)
        {
            var response = await _client.GetAsync($"{_configuration.GeoCodeURI}/json?address={cep}&sensor=true&key={_configuration.MapsKey}");

            response.EnsureSuccessStatusCode();

            var obj = await response.Content.ReadAsAsync<GoogleGeoCodeView>();

            if (!obj.status.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ReasonPhrase = "Endereço não encontrado"
                });

            return obj;
        }
    }
}
