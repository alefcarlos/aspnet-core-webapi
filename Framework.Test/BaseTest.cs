using Framework.Core.Serializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Framework.Test
{
    public abstract class BaseTest<T> where T : TestStartupBase
    {
        public BaseTest()
        {
            //Configuration
            var configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            Configuration = configurationBuilder;

            var services = new ServiceCollection();
            services.AddSingleton<JsonSerializerCommon>();
            services.AddLogging(opt => opt.AddConsole());

            ServiceProvider = services.BuildServiceProvider();

            var startup = Activator.CreateInstance<T>();

            startup.ConfigureServices(services, Configuration);
        }

        private IServiceProvider ServiceProvider { get; }

        private IServiceScope Scope => ServiceProvider.CreateScope();

        public IConfiguration Configuration { get; }
        public E GetService<E>() => Scope.ServiceProvider.GetRequiredService<E>();
    }
}