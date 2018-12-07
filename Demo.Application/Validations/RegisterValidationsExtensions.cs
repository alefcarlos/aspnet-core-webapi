using Demo.Application.Contracts.SignIn;
using Demo.Application.Contracts.SignUp;
using Demo.Application.Validations.SignIn;
using Demo.Application.Validations.SignUp;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application.Validations
{
    public static class RegisterValidationsExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            // can then manually register validators
            services.AddTransient<IValidator<SignInPostRequest>, SignInPostRequestValidator>();
            services.AddTransient<IValidator<SignUpPostRequest>, SignUpPostRequestValidator>();

            return services;
        }
    }
}
