using System;

using VideoGames.Application.Tests.Common;
using VideoGames.Domain.Entities;

using Xunit;

namespace VideoGames.Application.Tests.Requests;

public class BaseRequestTestsFixture : IDisposable
{
    public FakeAppDbContext DbContext { get; }

    public BaseRequestTestsFixture()
    {
        DbContext = FakeAppDbContextFactory.Create();

        AddInitialData();
    }

    public void Dispose()
    {
        DbContext?.Dispose();
        GC.SuppressFinalize(this);
    }

    private void AddInitialData()
    {
        DbContext.VideoGameGenres.AddRangeAsync(
            new VideoGameGenre()
            {
                Id = BaseRequestTestsConstants.VideoGameGenres.A.Id,
                Name = BaseRequestTestsConstants.VideoGameGenres.A.Name,
            },
            new VideoGameGenre()
            {
                Id = BaseRequestTestsConstants.VideoGameGenres.B.Id,
                Name = BaseRequestTestsConstants.VideoGameGenres.B.Name,
            },
            new VideoGameGenre()
            {
                Id = BaseRequestTestsConstants.VideoGameGenres.C.Id,
                Name = BaseRequestTestsConstants.VideoGameGenres.C.Name,
            }
        );

        DbContext.DeveloperStudios.AddRangeAsync(
            new DeveloperStudio()
            {
                Id = BaseRequestTestsConstants.DeveloperStudios.A.Id,
                Name = BaseRequestTestsConstants.DeveloperStudios.A.Name,
            },
            new DeveloperStudio()
            {
                Id = BaseRequestTestsConstants.DeveloperStudios.B.Id,
                Name = BaseRequestTestsConstants.DeveloperStudios.B.Name,
            },
            new DeveloperStudio()
            {
                Id = BaseRequestTestsConstants.DeveloperStudios.C.Id,
                Name = BaseRequestTestsConstants.DeveloperStudios.C.Name,
            }
        );

        DbContext.SaveChanges();
    }
}

[CollectionDefinition("RequestTestsCollection", DisableParallelization = true)]
public class RequestTestsCollection : ICollectionFixture<BaseRequestTestsFixture>
{
}
