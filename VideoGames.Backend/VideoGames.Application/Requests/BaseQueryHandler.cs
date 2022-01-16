using VideoGames.Application.Repositories;

namespace VideoGames.Application.Requests;

public abstract class BaseQueryHandler
{
    protected readonly IAppDbContext _dbContext;

    public BaseQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}
