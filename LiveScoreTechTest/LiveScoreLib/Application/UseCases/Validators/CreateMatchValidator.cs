using FluentValidation;

namespace LiveScoreLib.Application.UseCases.Validators;

public class CreateMatchValidator: AbstractValidator<CreateMatch>
{
    public CreateMatchValidator()
    {
        RuleFor(x => x.HomeTeam).NotNull().NotEmpty();
        RuleFor(x => x.AwayTeam).NotNull().NotEmpty();
    }
}