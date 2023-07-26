using FlitnexApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlitnexApi.Persistence;

public class MovieSchema : IEntityTypeConfiguration<EMovie>
{
    public void Configure(EntityTypeBuilder<EMovie> builder)
    {
        builder.Property(m => m.Title)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(m => m.Description)
            .HasMaxLength(500);
        
        builder.Property(m => m.VideoSourceUrl)
            .HasMaxLength(255);

        builder.Property(m => m.ImageUrl)
            .HasMaxLength(255);
        
        builder.HasMany(m => m.Genres).WithMany(g => g.Movies)
            .UsingEntity(j => j.ToTable("MovieAndGenre"));
            // .UsingEntity("MovieGenre",
            //     j => j.HasOne(typeof(EGenre)).WithMany().HasForeignKey("GenreId"),
            //     j => j.HasOne(typeof(EMovie)).WithMany().HasForeignKey("MovieId"),
            //     j =>
            //     {
            //         j.HasKey("MovieId", "GenreId");
            //         j.ToTable("MovieGenres");
            //     }
            // );

        builder.HasMany(m => m.Cast).WithOne(c => c.Movie)
            .HasForeignKey(c => c.MovieId);
    }
}