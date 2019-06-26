using System.Threading.Tasks;
using Refit;

namespace Demo.Core.ExternalServices.Google
{
    public interface IGoogleMapsAPI
    {
        [Get("/json?address={cep}&sensor=true&key={key}")]
        Task<GoogleGeoCodeView> SearchAsync(string cep, string key);
    }
}
