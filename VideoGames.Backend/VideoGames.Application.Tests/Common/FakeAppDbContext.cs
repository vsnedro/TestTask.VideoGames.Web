using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using VideoGames.Persistence.DbContexts;

namespace VideoGames.Application.Tests.Common;

public class FakeAppDbContext : AppDbContext
{
    private bool _disposed = false;

    public FakeAppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region IDisposable
    public override void Dispose()
    {
        if (_disposed) return;

        Database.EnsureDeleted();
        _disposed = true;

        base.Dispose();

        GC.SuppressFinalize(this);
    }

    public override async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        await base.DisposeAsync();

        Dispose();
    }
    #endregion
}
