using System.Threading.Tasks;
using Demo.Core.Contracts.Values;
using Framework.Services;

namespace Demo.Core.Services
{
    public interface IValuesServices
    {
        Task<ServicesResult> PostRabbitMessageAsync(PostMessageRequest request);
    }
}