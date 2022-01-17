using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using VideoGames.Application.Tests.Common;

using Xunit;

namespace VideoGames.Application.Tests.Requests.VideoGame.Commands;

[Collection("RequestTestsCollection")]
public abstract class BaseCommandHandlerTests
{
    protected FakeAppDbContext DbContext { get; }

    public BaseCommandHandlerTests(BaseRequestTestsFixture fixture)
    {
        DbContext = fixture.DbContext;
    }

    protected async Task AddVideoGameAAsync()
    {
        var developerStudio = await DbContext.DeveloperStudios
            .FirstAsync(x => x.Id == BaseVideoGameRequestTestsConstants.VideoGames.A.DeveloperStudioId);
        var videoGameGenres = await DbContext.VideoGameGenres
            .Where(x => BaseVideoGameRequestTestsConstants.VideoGames.A.GenreIds.Contains(x.Id))
            .ToListAsync();

        await DbContext.VideoGames.AddAsync(new Domain.Entities.VideoGame()
        {
            Id = BaseVideoGameRequestTestsConstants.VideoGames.A.Id,
            Name = BaseVideoGameRequestTestsConstants.VideoGames.A.Name,
            ReleaseDate = BaseVideoGameRequestTestsConstants.VideoGames.A.ReleaseDate,
            DeveloperStudio = developerStudio,
            Genres = videoGameGenres
        });

        await DbContext.SaveChangesAsync();
    }

    protected async Task RemoveVideoGamesAsync()
    {
        DbContext.VideoGames.RemoveRange(DbContext.VideoGames);
        await DbContext.SaveChangesAsync();
    }
}
