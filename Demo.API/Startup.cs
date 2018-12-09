using Demo.Application.Data.MongoDB;
using Demo.Application.Data.MySql;
using Demo.Application.GraphQL;
using Demo.Application.Services;
using Demo.Application.Validations;
using Framework.Data.MongoDB;
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
        }

        public override void BeforeConfigureAppMVC(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UsePathBase("/api");

            app.AddGraphQLTypes();
        }
    }
}
