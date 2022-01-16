using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using VideoGames.Domain.Entities;

namespace VideoGames.Persistence.DbContexts.EntityTypeConfigurations;

internal class VideoGameConfiguration : IEntityTypeConfiguration<VideoGame>
{
    public void Configure(EntityTypeBuilder<VideoGame> builder)
    {
        builder.ToTable("VideoGames");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(x => x.ReleaseDate).IsRequired();

        builder.HasOne(x => x.DeveloperStudio)
            .WithMany(s => s.VideoGames)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Genres)
            .WithMany(g => g.VideoGames);
    }
}
