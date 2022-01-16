using MediatR;

using VideoGames.Domain.Entities;
using VideoGames.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using VideoGames.Application.Exceptions;

namespace VideoGames.Application.Requests.VideoGame.Commands.CreateVideoGame;

public class CreateVideoGameCommandHandler : BaseCommandHandler, IRequestHandler<CreateVideoGameCommand, int>
{
    public CreateVideoGameCommandHandler(IAppDbContext dbContext)
        : base(dbContext) { }

    public async Task<int> Handle(CreateVideoGameCommand request, CancellationToken cancellationToken = default)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var developer = await _dbContext.DeveloperStudios
            .FirstOrDefaultAsync(x => x.Id == request.DeveloperStudioId, cancellationToken);
        _ = developer ?? throw new EntityNotFoundException(nameof(DeveloperStudio), request.DeveloperStudioId);

        var genres = await _dbContext.VideoGameGenres
            .Where(x => request.GenresId.Contains(x.Id))
            .ToListAsync(cancellationToken);
        if (!genres.Any())
        {
            throw new EntityNotFoundException(nameof(VideoGameGenre), request.GenresId);
        }

        var game = new Domain.Entities.VideoGame()
        {
            Name = request.Name,
            ReleaseDate = request.ReleaseDate,
            DeveloperStudio = developer,
            Genres = genres,
        };

        await _dbContext.VideoGames.AddAsync(game, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}
