using Microsoft.EntityFrameworkCore;

using VideoGames.Domain.Entities;

namespace VideoGames.Application.Repositories;

public interface IAppDbContext
{
    public DbSet<DeveloperStudio> DeveloperStudios { get; set; }
    public DbSet<VideoGame> VideoGames { get; set; }
    public DbSet<VideoGameGenre> VideoGameGenres { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken token = default);
}
