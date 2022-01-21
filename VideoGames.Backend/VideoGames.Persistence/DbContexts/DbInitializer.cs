namespace VideoGames.Persistence.DbContexts;

public static class DbInitializer
{
    public static void Initialize(AppDbContext dbContext)
    {
        _ = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        dbContext.Database.EnsureCreated();
    }

    public static async Task InitializeAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
    {
        _ = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);

        if (!dbContext.VideoGameGenres.Any())
        {
            await dbContext.VideoGameGenres.AddRangeAsync(new List<Domain.Entities.VideoGameGenre>() {
                new Domain.Entities.VideoGameGenre() { Name = "Adventure" },
                new Domain.Entities.VideoGameGenre() { Name = "Arcade" },
                new Domain.Entities.VideoGameGenre() { Name = "Fighting" },
                new Domain.Entities.VideoGameGenre() { Name = "Puzzle" },
                new Domain.Entities.VideoGameGenre() { Name = "Quest" },
                new Domain.Entities.VideoGameGenre() { Name = "Race" },
                new Domain.Entities.VideoGameGenre() { Name = "RPG" },
                new Domain.Entities.VideoGameGenre() { Name = "Shooter" },
                new Domain.Entities.VideoGameGenre() { Name = "Simulation" },
                new Domain.Entities.VideoGameGenre() { Name = "Strategy" },
            },
            cancellationToken); 
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        
        if (!dbContext.DeveloperStudios.Any())
        {
            await dbContext.DeveloperStudios.AddRangeAsync(new List<Domain.Entities.DeveloperStudio>() {
                new Domain.Entities.DeveloperStudio() { Name = "Activision" },
                new Domain.Entities.DeveloperStudio() { Name = "Blizzard" },
                new Domain.Entities.DeveloperStudio() { Name = "Bethesda" },
                new Domain.Entities.DeveloperStudio() { Name = "Capcom" },
                new Domain.Entities.DeveloperStudio() { Name = "Electronic Arts" },
                new Domain.Entities.DeveloperStudio() { Name = "Microsoft" },
                new Domain.Entities.DeveloperStudio() { Name = "Nintendo" },
                new Domain.Entities.DeveloperStudio() { Name = "Sega" },
                new Domain.Entities.DeveloperStudio() { Name = "Sony" },
                new Domain.Entities.DeveloperStudio() { Name = "Ubisoft" },
            },
            cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
