using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VideoGames.Application.Repositories;

namespace VideoGames.Application.Requests;

public abstract class BaseQueryHandler
{
    protected IAppDbContext _dbContext;

    public BaseQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}
