using MediatR;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameList;

public class GetVideoGameListQuery : IRequest<VideoGameListVm>
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int? ReleaseYear { get; set; }

    public int? DeveloperStudioId { get; set; }

    public int? VideoGameGenreId { get; set; }
}
