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
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false)
                .Build();

            Configuration = configurationBuilder;

            var services = new ServiceCollection();
            
            services.AddSingleton(Configuration);

            services.AddSingleton<JsonSerializerCommon>();
            services.AddLogging(opt => opt.AddConsole());

            var startup = Activator.CreateInstance<T>();

            startup.ConfigureServices(services, Configuration);

            ServiceProvider = services.BuildServiceProvider();
        }

        private IServiceProvider ServiceProvider { get; }

        private IServiceScope Scope => ServiceProvider.CreateScope();

        public IConfiguration Configuration { get; }
        public E GetService<E>() => Scope.ServiceProvider.GetRequiredService<E>();
    }
}