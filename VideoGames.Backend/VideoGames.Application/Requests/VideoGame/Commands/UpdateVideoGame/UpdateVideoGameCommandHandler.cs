using MediatR;

using Microsoft.EntityFrameworkCore;

using VideoGames.Domain.Entities;
using VideoGames.Application.Repositories;
using VideoGames.Application.Exceptions;

namespace VideoGames.Application.Requests.VideoGame.Commands.UpdateVideoGame;

public class UpdateVideoGameCommandHandler : BaseCommandHandler, IRequestHandler<UpdateVideoGameCommand>
{
    public UpdateVideoGameCommandHandler(IAppDbContext dbContext)
        : base(dbContext) { }

    public async Task<Unit> Handle(UpdateVideoGameCommand request, CancellationToken cancellationToken = default)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var game = await _dbContext.VideoGames
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        _ = game ?? throw new EntityNotFoundException(nameof(Domain.Entities.VideoGame), request.Id);

        var developer = await _dbContext.DeveloperStudios
            .FirstOrDefaultAsync(x => x.Id == request.DeveloperStudioId, cancellationToken);
        _ = developer ?? throw new EntityNotFoundException(nameof(DeveloperStudio), request.DeveloperStudioId);

        var genres = await _dbContext.VideoGameGenres
            .Where(x => request.GenreIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
        if (!genres.Any())
        {
            throw new EntityNotFoundException(nameof(VideoGameGenre), request.GenreIds);
        }

        (game.Name, game.ReleaseDate, game.DeveloperStudio, game.Genres) = (request.Name, request.ReleaseDate, developer, genres);

        await _dbContext.VideoGames.AddAsync(game, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
