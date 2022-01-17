using System;

using Microsoft.EntityFrameworkCore;

using VideoGames.Persistence.DbContexts;

namespace VideoGames.Application.Tests.Common;

public static class FakeAppDbContextFactory
{
    public static FakeAppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var dbContext = new FakeAppDbContext(options);
        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}
