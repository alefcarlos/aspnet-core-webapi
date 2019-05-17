using System.IO;
using Demo.Consumer.HandleMessages.Tasks;
using dotenv.net;
using Framework.Core.Serializer;
using Framework.MessageBroker.RabbitMQ;
using Framework.WebAPI.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Consumer.HandleMessages
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            if (File.Exists(".env"))
                DotEnv.Config();

            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<JsonSerializerCommon>();
            services.AddHealthCheck();

            services.AddRabbitBroker("demo.consumer");
            services.AddHostedService<TesteMessageHandlerTask>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthCheck();
        }
    }
}
