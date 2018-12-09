using Demo.Core.Contracts.DragonBall.Request;
using Demo.Core.Contracts.SignIn;
using Demo.Core.Contracts.SignUp;
using Demo.Core.Validations.DragonBall;
using Demo.Core.Validations.SignIn;
using Demo.Core.Validations.SignUp;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core.Validations
{
    public static class RegisterValidationsExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            // can then manually register validators
            services.AddTransient<IValidator<SignInPostRequest>, SignInPostRequestValidator>();
            services.AddTransient<IValidator<SignUpPostRequest>, SignUpPostRequestValidator>();
            services.AddTransient<IValidator<DragonBallPostRequest>, DragonBallPostRequestValidator>();
            services.AddTransient<IValidator<DragonBallPostRelativeRequest>, DragonBallPostRelativeRequestValidator>();

            return services;
        }
    }
}
