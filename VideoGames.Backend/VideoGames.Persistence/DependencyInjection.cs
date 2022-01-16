using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using VideoGames.Application.Repositories;
using VideoGames.Persistence.DbContexts;

namespace VideoGames.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        const string DefaultConnectionName = "DefaultConnection";

        var connectionString = configuration.GetConnectionString(DefaultConnectionName);
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        return services;
    }
}
