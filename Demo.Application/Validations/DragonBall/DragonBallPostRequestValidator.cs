using Demo.Application.Contracts.DragonBall.Request;
using FluentValidation;

namespace Demo.Application.Validations.DragonBall
{
    public class DragonBallPostRequestValidator : AbstractValidator<DragonBallPostRequest>
    {
        public DragonBallPostRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do personagem é obrigatório.");

            RuleFor(x => x.Kind)
                .IsInEnum()
                .WithMessage("A raça do personagem deve ser válida!");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("A informação da data de nascimento é obrigatória!");
        }
    }
}
