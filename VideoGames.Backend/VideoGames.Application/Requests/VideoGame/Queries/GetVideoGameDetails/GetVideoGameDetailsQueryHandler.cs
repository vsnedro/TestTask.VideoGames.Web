using MediatR;

using Microsoft.EntityFrameworkCore;

using VideoGames.Application.Exceptions;
using VideoGames.Application.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace VideoGames.Application.Requests.VideoGame.Queries.GetVideoGameDetails;

public class GetVideoGameDetailsQueryHandler : BaseQueryHandler, IRequestHandler<GetVideoGameDetailsQuery, VideoGameDetailsVm>
{
    private const int MemoryCacheMinutes = 5;
    private readonly IMemoryCache _memoryCache;

    public GetVideoGameDetailsQueryHandler(IAppDbContext dbContext, IMemoryCache memoryCache)
        : base(dbContext)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public async Task<VideoGameDetailsVm> Handle(GetVideoGameDetailsQuery request, CancellationToken cancellationToken = default)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        if (!_memoryCache.TryGetValue(request.Id, out Domain.Entities.VideoGame? game))
        {
            game = await _dbContext.VideoGames
                .Include(x => x.DeveloperStudio)
                .Include(x => x.Genres)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            _ = game ?? throw new EntityNotFoundException(nameof(VideoGame), request.Id);

            if (!cancellationToken.IsCancellationRequested)
            {
                _memoryCache.Set(request.Id, game, TimeSpan.FromMinutes(MemoryCacheMinutes));
            }
        }

        return new VideoGameDetailsVm()
        {
            Id = game!.Id,
            Name = game.Name,
            ReleaseDate = game.ReleaseDate,
            DeveloperStudioName = game.DeveloperStudio.Name,
            GenreNames = game.Genres.Select(x => x.Name),
        };
    }
}
