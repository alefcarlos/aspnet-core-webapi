using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Test
{
    public abstract class TestStartupBase
    {
        public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}