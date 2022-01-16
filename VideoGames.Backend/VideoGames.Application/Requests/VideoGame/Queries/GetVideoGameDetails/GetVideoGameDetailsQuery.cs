using MediatR;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameDetails;

public class GetVideoGameDetailsQuery : IRequest<VideoGameDetailsVm>
{
    public int Id { get; set; }
}
