using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using VideoGames.Application.Tests.Common;

using Xunit;

namespace VideoGames.Application.Tests.Requests.VideoGame.Queries;

[Collection("RequestTestsCollection")]
public abstract class BaseQueryHandlerTests
{
    protected FakeAppDbContext DbContext { get; }
    protected IMemoryCache MemoryCache { get; }

    public BaseQueryHandlerTests(BaseRequestTestsFixture fixture)
    {
        DbContext = fixture.DbContext;
        MemoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
    }

    protected async Task AddVideoGamesAsync()
    {
        await AddVideoGameAsync(
            BaseVideoGameRequestTestsConstants.VideoGames.A.Id,
            BaseVideoGameRequestTestsConstants.VideoGames.A.Name,
            BaseVideoGameRequestTestsConstants.VideoGames.A.ReleaseDate,
            BaseVideoGameRequestTestsConstants.VideoGames.A.DeveloperStudioId,
            BaseVideoGameRequestTestsConstants.VideoGames.A.GenreIds,
            saveChanges: false);

        await AddVideoGameAsync(
            BaseVideoGameRequestTestsConstants.VideoGames.B.Id,
            BaseVideoGameRequestTestsConstants.VideoGames.B.Name,
            BaseVideoGameRequestTestsConstants.VideoGames.B.ReleaseDate,
            BaseVideoGameRequestTestsConstants.VideoGames.B.DeveloperStudioId,
            BaseVideoGameRequestTestsConstants.VideoGames.B.GenreIds,
            saveChanges: false);

        await AddVideoGameAsync(
            BaseVideoGameRequestTestsConstants.VideoGames.C.Id,
            BaseVideoGameRequestTestsConstants.VideoGames.C.Name,
            BaseVideoGameRequestTestsConstants.VideoGames.C.ReleaseDate,
            BaseVideoGameRequestTestsConstants.VideoGames.C.DeveloperStudioId,
            BaseVideoGameRequestTestsConstants.VideoGames.C.GenreIds,
            saveChanges: false);

        await DbContext.SaveChangesAsync();
    }

    protected async Task AddVideoGameAsync(
        int id, string name, DateTime releaseDate, int developerStudioId, IEnumerable<int> genreIds, bool saveChanges = true)
    {
        var developerStudio = await DbContext.DeveloperStudios
            .FirstAsync(x => x.Id == developerStudioId);

        var videoGameGenres = await DbContext.VideoGameGenres
            .Where(x => genreIds.Contains(x.Id))
            .ToListAsync();

        await DbContext.VideoGames.AddAsync(new Domain.Entities.VideoGame()
        {
            Id = id,
            Name = name,
            ReleaseDate = releaseDate,
            DeveloperStudio = developerStudio,
            Genres = videoGameGenres
        });

        if (saveChanges)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    protected async Task RemoveVideoGamesAsync()
    {
        DbContext.VideoGames.RemoveRange(DbContext.VideoGames);
        await DbContext.SaveChangesAsync();
    }
}
