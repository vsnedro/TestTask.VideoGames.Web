using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameList;

public class GetVideoGameListQuery : IRequest<VideoGameListVm>
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int? ReleaseYear { get; set; }

    public int? DeveloperStudioId { get; set; }

    public int? VideoGameGenreId { get; set; }
}
