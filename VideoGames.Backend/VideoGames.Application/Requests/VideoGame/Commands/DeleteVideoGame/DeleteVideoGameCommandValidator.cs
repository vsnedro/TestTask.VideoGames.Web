using FluentValidation;

namespace VideoGames.Application.Requests.VideoGame.Commands.DeleteVideoGame;

public class DeleteVideoGameCommandValidator : AbstractValidator<DeleteVideoGameCommand>
{
    public DeleteVideoGameCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
