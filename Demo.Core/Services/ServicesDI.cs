using Demo.Core.Services.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core.Services
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
            services.AddSingleton<IValuesServices, ValuesServices>();

            services.AddScoped<ICharacterServices, CharacterServices>();
            
            //GraphQL Services
            services.AddGraphServices();

            return services;
        }
    }
}
