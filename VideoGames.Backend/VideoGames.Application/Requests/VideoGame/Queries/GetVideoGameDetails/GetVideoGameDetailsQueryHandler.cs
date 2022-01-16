using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using VideoGames.Domain.Entities;
using VideoGames.Application.Exceptions;
using VideoGames.Application.Repositories;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameDetails;

public class GetVideoGameDetailsQueryHandler : BaseQueryHandler, IRequestHandler<GetVideoGameDetailsQuery, VideoGameDetailsVm>
{
    public GetVideoGameDetailsQueryHandler(IAppDbContext dbContext)
        : base(dbContext) { }

    public async Task<VideoGameDetailsVm> Handle(GetVideoGameDetailsQuery request, CancellationToken cancellationToken = default)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var game = await _dbContext.VideoGames
            .Include(x => x.DeveloperStudio)
            .Include(x => x.Genres)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        _ = game ?? throw new EntityNotFoundException(nameof(VideoGame), request.Id);

        return new VideoGameDetailsVm()
        {
            Id = game.Id,
            Name = game.Name,
            ReleaseDate = game.ReleaseDate,
            DeveloperStudioName = game.DeveloperStudio.Name,
            GenreNames = game.Genres.Select(x => x.Name),
        };
    }
}
