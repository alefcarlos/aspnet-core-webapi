using Application.Services;
using Clinfy.Application.Data.MongoDB;
using Framework.Data.MongoDB;
using Framework.WebAPI.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinfy
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void AfterConfigureServices(IServiceCollection services)
        {
            //Services
            services.AddServices();

            //Repositories
            services.AddMongoDB();
            services.AddMongoRepositories();
        }

        public override void BeforeConfigureAppMVC(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
