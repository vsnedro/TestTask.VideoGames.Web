using MediatR;

using Microsoft.EntityFrameworkCore;

using VideoGames.Application.Repositories;
using VideoGames.Application.Exceptions;

namespace VideoGames.Application.Requests.VideoGame.Commands.DeleteVideoGame;

public class DeleteVideoGameCommandHandler : BaseCommandHandler, IRequestHandler<DeleteVideoGameCommand>
{
    public DeleteVideoGameCommandHandler(IAppDbContext dbContext)
        : base(dbContext) { }

    public async Task<Unit> Handle(DeleteVideoGameCommand request, CancellationToken cancellationToken = default)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var game = await _dbContext.VideoGames
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        _ = game ?? throw new EntityNotFoundException(nameof(Domain.Entities.VideoGame), request.Id);

        _dbContext.VideoGames.Remove(game);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
