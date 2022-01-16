using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
