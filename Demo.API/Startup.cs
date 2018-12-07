using Demo.Application.Data.MongoDB;
using Demo.Application.GraphQL;
using Demo.Application.Services;
using Demo.Application.Validations;
using Framework.Data.MongoDB;
using Framework.WebAPI.Hosting;
using GraphiQl;
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

            //GraphQL
            services.AddGraphQLModels();
        }

        public override void BeforeConfigureAppMVC(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphiQl();
        }
    }
}
