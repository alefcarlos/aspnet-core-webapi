using Microsoft.Extensions.DependencyInjection;

namespace Framework.WebAPI.Versioning
{
    public static class ApiVersionExtensions
    {
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(o => o.ReportApiVersions = true);


            return services;
        }

        //public static IApplicationBuilder UseApiVersion(this IApplicationBuilder app)
        //{
        //    // Enable middleware to serve generated Swagger as a JSON endpoint.
        //    app.UseSwagger();

        //    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
        //    // specifying the Swagger JSON endpoint.
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //    });

        //    return app;
        //}
    }
}
