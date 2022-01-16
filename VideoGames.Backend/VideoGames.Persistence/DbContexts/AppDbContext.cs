using System.Reflection;

using Microsoft.EntityFrameworkCore;

using VideoGames.Application.Repositories;
using VideoGames.Domain.Entities;

namespace VideoGames.Persistence.DbContexts;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<DeveloperStudio> DeveloperStudios { get; set; }
    public DbSet<VideoGame> VideoGames { get; set; }
    public DbSet<VideoGameGenre> VideoGameGenres { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
