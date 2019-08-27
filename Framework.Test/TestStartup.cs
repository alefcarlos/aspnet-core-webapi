using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Test
{
    public abstract class TestStartup
    {
        public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}