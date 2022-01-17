using MediatR;

namespace VideoGames.Application.Requests.VideoGame.Commands.UpdateVideoGame;

public class UpdateVideoGameCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int DeveloperStudioId { get; set; }
    public IEnumerable<int> GenreIds { get; set; } = new List<int>();
}
