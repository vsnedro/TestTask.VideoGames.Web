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
        RuleFor(x => x.GenresId).NotEmpty();
        RuleForEach(x => x.GenresId).GreaterThan(0);
    }
}
