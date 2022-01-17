using FluentValidation;

namespace VideoGames.Application.Requests.VideoGame.Commands.UpdateVideoGame;

public class UpdateVideoGameCommandValidator : AbstractValidator<UpdateVideoGameCommand>
{
    public UpdateVideoGameCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.ReleaseDate).NotEmpty();
        RuleFor(x => x.DeveloperStudioId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.GenreIds).NotEmpty();
        RuleForEach(x => x.GenreIds).GreaterThan(0);
    }
}
