using dotenv.net;
using Framework.WebAPI.Documetation;
using Framework.WebAPI.Hosting.JWT;
using Framework.WebAPI.Hosting.Middlewares;
using Framework.WebAPI.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Framework.WebAPI.Hosting
{
    public abstract class BaseStartup
    {
        public BaseStartup(IConfiguration configuration)
        {
            if (File.Exists(".env"))
                DotEnv.Config();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSecurity();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddApiVersion();
            services.AddDocumentation();

            AfterConfigureServices(services);
        }

        public abstract void AfterConfigureServices(IServiceCollection services);


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {

            BeforeConfigureAppMVC(app, env);

            app.UseMiddleware<HttpExceptionMiddleware>()
                .UseMvc();

            app.UseDocumentation(provider);
        }

        public abstract void BeforeConfigureAppMVC(IApplicationBuilder app, IHostingEnvironment env);
    }
}
