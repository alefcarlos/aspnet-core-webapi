using Demo.Application.Services.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application.Services
{
    /// <summary>
    /// Reponsável por adicionar as injeções de dependências de serviços
    /// </summary>
    public static class ServicesDI
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ISignInServices, SignInServices>();
            services.AddSingleton<ISignUpServices, SignUpServices>();
            services.AddSingleton<IProfileServices, ProfileServices>();


            //GraphQL Services
            services.AddGraphServices();

            return services;
        }
    }
}
