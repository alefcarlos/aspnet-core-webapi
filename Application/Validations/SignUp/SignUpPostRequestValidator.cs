using Application.Contracts.SignUp;
using FluentValidation;
using System;

namespace Application.Validations.SignUp
{
    public class SignUpPostRequestValidator : AbstractValidator<SignUpPostRequest>
    {
        public SignUpPostRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome completo é obrigatóio");

            RuleFor(x => x.BornDate)
                .NotEqual(DateTime.MinValue)
                .WithMessage("Data de nascimento é obrigatória!");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage("Valores possíveis para Sexo: 0 - Masculino, 1 - Feminino");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-mail é obrigatório!")
                .EmailAddress()
                .WithMessage("O e-mail informado é inválido!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatória!")
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Senhas não conferem!");
        }
    }
}
