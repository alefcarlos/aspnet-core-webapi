using Application.Contracts.SignIn;
using FluentValidation;

namespace Application.Validations.SignIn
{
    public class SignInPostRequestValidator : AbstractValidator<SignInPostRequest>
    {
        public SignInPostRequestValidator()
        {
            //Validar GrantType
            RuleFor(x => x.GrantType)
                .Must(ValidGrantType)
                .WithMessage("Valores possíveis para grantType são: password e refresh_token");

            //Validar refresh_token
            When(x => x.GrantType == "refresh_token", () =>
            {
                RuleFor(x => x.Email)
                    .Empty()
                    .WithMessage("Quando grantType for refresh_token, não é possível passar email nem senha!");

                RuleFor(x => x.Password)
                    .Empty()
                    .WithMessage("Quando grantType for refresh_token, não é possível passar email nem senha!");

                RuleFor(x => x.RefreshToken)
                    .NotEmpty()
                    .WithMessage("Quando grantType for refresh_token, é obrigatório passar o valor de refreshToken!");
            });

            //Validar password
            When(x => x.GrantType == "password", () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Quando grantType for password, email e password são obrigatórios!");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Quando grantType for password, email e password são obrigatórios!");

                RuleFor(x => x.RefreshToken)
                    .Empty()
                    .WithMessage("Quando grantType for password, não é possível passar o valor de refreshToken!");
            });
        }

        private bool ValidGrantType(string value) => string.Equals(value, "password") || string.Equals(value, "refresh_token");
    }
}
