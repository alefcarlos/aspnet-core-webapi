using Demo.Core.Data.MongoDB;
using Demo.Core.Data.MySql;
using Demo.Core.GraphQL;
using Demo.Core.Services;
using Demo.Core.Validations;
using Framework.Data.CacheProviders;
using Framework.Data.MongoDB;
using Framework.MessageBroker.RabbitMQ;
using Framework.WebAPI.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.API
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void AfterConfigureServices(IServiceCollection services)
        {
            //Adicioando as validações
            services.AddValidators();

            //Services
            services.AddServices();

            //Repositories
            services.AddMongoDB();
            services.AddMongoRepositories();

            services.AddMySql();

            //GraphQL
            services.AddGraphQLTypes();

            //Redis
            services.AddRedis();

            //RabbitMQ
            services.AddRabbitMQ("demo.api");
        }

        public override void BeforeConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
        }

        public override void AfterConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddGraphQLTypes();
        }
    }
}
