using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Services
{
    /// <summary>
    /// Classe base para os serviços
    /// </summary>
    public class BaseServices
    {
        public ServicesResult Ok() => new ServicesResult(true, System.Net.HttpStatusCode.OK);

        public ServicesResult Ok(object data) => new ServicesResult(true, System.Net.HttpStatusCode.OK, data);

        public ServicesResult BadRequest(string message) => new ServicesResult(false, System.Net.HttpStatusCode.BadRequest, message);

        public ServicesResult NotFound(string message) => new ServicesResult(false, System.Net.HttpStatusCode.NotFound, message);

        public ServicesResult Conflict(string message) => new ServicesResult(false, System.Net.HttpStatusCode.Conflict, message);

        public ServicesResult Created() => new ServicesResult(true, System.Net.HttpStatusCode.Created);
    }
}
