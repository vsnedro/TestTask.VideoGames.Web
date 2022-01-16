using MediatR;

namespace VideoGames.Application.Requests.VideoGame.Commands.DeleteVideoGame;

public class DeleteVideoGameCommand : IRequest
{
    public int Id { get; set; }
}
