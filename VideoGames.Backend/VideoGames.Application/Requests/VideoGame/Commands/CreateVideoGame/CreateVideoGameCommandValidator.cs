using FluentValidation;

namespace VideoGames.Application.Requests.VideoGame.Commands.CreateVideoGame;

public class CreateVideoGameCommandValidator : AbstractValidator<CreateVideoGameCommand>
{
    public CreateVideoGameCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.ReleaseDate).NotEmpty();
        RuleFor(x => x.DeveloperStudioId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.GenreIds).NotEmpty();
        RuleForEach(x => x.GenreIds).GreaterThan(0);
    }
}
