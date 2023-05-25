using FluentValidation;

namespace LiveScoreLib.Application.UseCases.Validators;

public class UpdateScoreValidator: AbstractValidator<UpdateScore>
{
    public UpdateScoreValidator()
    {
        RuleFor(x => x.HomeTeamScore).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.AwayTeamScore).NotNull().GreaterThanOrEqualTo(0);
    }
}