using Demo.Application.Contracts.DragonBall.Request;
using FluentValidation;

namespace Demo.Application.Validations.DragonBall
{
    public class DragonBallPostRelativeRequestValidator : AbstractValidator<DragonBallPostRelativeRequest>
    {
        public DragonBallPostRelativeRequestValidator()
        {
            RuleFor(x => x.RelativeId)
                .NotEmpty()
                .WithMessage("O ID do personagem é obrigatório.");

            RuleFor(x => x.Kind)
                .IsInEnum()
                .WithMessage("A raça do personagem deve ser válida!");
        }
    }
}
