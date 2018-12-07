using Application.Contracts.SignIn;
using Application.Contracts.SignUp;
using Application.Validations.SignIn;
using Application.Validations.SignUp;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validations
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
