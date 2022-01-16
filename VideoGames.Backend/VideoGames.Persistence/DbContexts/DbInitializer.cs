namespace VideoGames.Persistence.DbContexts;

public static class DbInitializer
{
    public static void Initialize(AppDbContext dbContext)
    {
        _ = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        dbContext.Database.EnsureCreated();
    }

    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        _ = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        await dbContext.Database.EnsureCreatedAsync();
    }
}
