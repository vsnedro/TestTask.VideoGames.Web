using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using VideoGames.Application.Repositories;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameList;

public class GetVideoGameListQueryHandler : BaseQueryHandler, IRequestHandler<GetVideoGameListQuery, VideoGameListVm>
{
    public GetVideoGameListQueryHandler(IAppDbContext dbContext)
        : base(dbContext) { }

    public async Task<VideoGameListVm> Handle(GetVideoGameListQuery request, CancellationToken cancellationToken = default)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var query = _dbContext.VideoGames.AsQueryable();
        if (request.ReleaseYear.HasValue)
        {
            query = query.Where(x => x.ReleaseDate.Year == request.ReleaseYear);
        }
        if (request.DeveloperStudioId.HasValue)
        {
            query = query.Where(x => x.DeveloperStudio.Id == request.DeveloperStudioId);
        }
        if (request.VideoGameGenreId.HasValue)
        {
            query = query.Where(x => x.Genres.Any(g => g.Id == request.VideoGameGenreId));
        }

        var count = await query.CountAsync(cancellationToken);
        var games = await query
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(game => new VideoGameLookupDto
            {
                Id = game.Id,
                Name = game.Name,
                ReleaseDate = game.ReleaseDate,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new VideoGameListVm()
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling(count / (double)request.PageSize),
            VideoGames = games
        };
    }
}
