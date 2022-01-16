using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using VideoGames.Domain.Entities;

namespace VideoGames.Persistence.DbContexts.EntityTypeConfigurations;

internal class VideoGameGenreConfiguration : IEntityTypeConfiguration<VideoGameGenre>
{
    public void Configure(EntityTypeBuilder<VideoGameGenre> builder)
    {
        builder.ToTable("VideoGameGenres");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}
