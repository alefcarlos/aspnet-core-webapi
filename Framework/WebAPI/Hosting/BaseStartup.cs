using dotenv.net;
using FluentValidation.AspNetCore;
using Framework.Core.Serializer;
using Framework.WebAPI.Documetation;
using Framework.WebAPI.HealthCheck;
using Framework.WebAPI.Hosting.Cors;
using Framework.WebAPI.Hosting.Formatters;
using Framework.WebAPI.Hosting.JWT;
using Framework.WebAPI.Hosting.Middlewares;
using Framework.WebAPI.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            services.AddCustomCors();

            services.AddHealthCheck();

            services.AddMvc(o => o.InputFormatters.Add(new ImageRawRequestBodyFormatter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation();
                //.ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

            services.AddApiVersion();
            services.AddDocumentation();

            services.AddSingleton<JsonSerializer>();

            AfterConfigureServices(services);
        }

        public abstract void AfterConfigureServices(IServiceCollection services);


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            BeforeConfigureApp(app, env);

            app.UseCustomCors();

            app.UseHealthCheck();

            app.UseMiddleware<HttpExceptionMiddleware>()
                .UseMvc();

            app.UseAuthentication();

            app.UseDocumentation(provider);

            AfterConfigureApp(app, env);

        }

        public abstract void BeforeConfigureApp(IApplicationBuilder app, IHostingEnvironment env);

        public abstract void AfterConfigureApp(IApplicationBuilder app, IHostingEnvironment env);
    }
}
